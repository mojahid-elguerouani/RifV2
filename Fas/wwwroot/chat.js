"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();


connection.on("refreshOnlineUsers", function () {
    refreshOnlineUsers();
});
//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
    refreshOnlineUsers();
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, "touserid",message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

connection.on("receiveNotification", function (notificationType, userInfo, notificationID, notificationCounts) {
    changeUserNotificationCounts(notificationCounts);
    var notificationMessage = '';
    if (notificationType == "FriendRequest") {
        notificationMessage = '<span><a href="/User/Profile/' + userInfo.UserID + '"><img src="' + userInfo.ProfilePicture + '" class="profilePictureCircle" />&nbsp;&nbsp;&nbsp;' + userInfo.Name + '</a><br /><span class="pull-right"><span style="font-size:11px;">Friend Request</span> &nbsp;&nbsp;<input type="button" class="btn btn-success btn-xs request-response" data-user-id="' + userInfo.UserID + '" data-value="Accepted" value="Accept" />&nbsp;&nbsp;<input type="button" class="btn btn-danger btn-xs request-response" data-user-id="' + userInfo.UserID + '" data-value="Rejected" value="Reject" /></span><br /></span>';
    }
    else if (notificationType == "FriendRequestAccepted") {
        notificationMessage = '<span><a href="/User/Profile/' + userInfo.UserID + '"><img src="' + userInfo.ProfilePicture + '" class="profilePictureCircle" />&nbsp;&nbsp;&nbsp;' + userInfo.Name + '</a><br /><span class="pull-right"><span style="font-size:11px;">Accepted your request</span></span><br /></span>';
    }
    var notificationHtml = '<div data-notificationID="' + notificationID + '" style="display:none" id="divNotificationPopUp-' + notificationID + '" class="alert alert-dismissible alert-info divNotificationPopup"><button type="button" class="close btnCloseNotification" data-notificationID="' + notificationID + '">&times;</button>' + notificationMessage + '</div>';
    $('.new-notificaion-window').append(notificationHtml);
    $(document).find('#divNotificationPopUp-' + notificationID + '').animate({ "opacity": "show", top: "100" }, 500);
    setTimeout(function () {
        removeNotificationPop(notificationID);
    }, 60000)
});
///

connection.on("receiveNotificationUpdate", function (notificationmsg, ProfilePicture, Name, notificationID, notificationCounts) {
    changeUserNotificationCounts(notificationCounts);
    var notificationMessage = '';
    notificationMessage = '<li class="notification"><div class="media"> <img class="img-radius" src="' + ProfilePicture + '" alt="Generic placeholder image"> <div class="media-body"><p><strong>' + Name + '</strong><span class="n-time text-muted"><i class="icon feather icon-clock m-r-10"></i>30 min</span></p><p>' + notificationmsg + '</p></div> </div>   </li>'

    var notificationHtml = '<div data-notificationID="' + notificationID + '" style="display:none" id="divNotificationPopUp-' + notificationID + '" class="alert alert-dismissible alert-info divNotificationPopup"><button type="button" class="close btnCloseNotification" data-notificationID="' + notificationID + '">&times;</button>' + notificationMessage + '</div>';
    $('.new-notificaion-window').append(notificationHtml);
    $(document).find('#divNotificationPopUp-' + notificationID + '').animate({ "opacity": "show", top: "100" }, 500);
    setTimeout(function () {
        removeNotificationPop(notificationID);
    }, 60000)
});
///


connection.on("refreshNotificationCounts", function (notificationCounts) {
    changeUserNotificationCounts(notificationCounts)
});


connection.on("ReceiveTyper", function (fromUserId) {
    var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
    if (currentChatUserID == fromUserId) {
        $(document).find('div.chat-user-status').html('يكتب الان...');
        setTimeout(function () {
            $(document).find('div.chat-user-status').html('');
        }, 1000);
    }
});

connection.on("AddNewChatMessage", function (messageRow, fromUserId, toUserId, fromUserName, fromUserProfilePic, toUserName, toUserProfilePic) {
    {

        var currentUserId = $('#hdfLoggedInUserID').val();
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();

        if (currentChatUserID == fromUserId || currentChatUserID == toUserId) {
            createNewMessageBlock(fromUserName, fromUserProfilePic, messageRow.createdOn, messageRow.message, (currentUserId == fromUserId ? 'right' : 'left'), messageRow.chatMessageID, messageRow.status);
            if (currentUserId != fromUserId) {
                var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
                setTimeout(function () {
                    UpdateChatMessageStatus(messageRow.chatMessageID, currentChatUserID);//, projectID
                }, 100);
            }
        }
        if (currentUserId != fromUserId) {
            var windowActive = $('#hdfWindowIsActiveOrNot').val();
            if (windowActive == 'False') {
                document.title = "Message received from " + fromUserName;
            }
        }
        addChatMessageCount(currentUserId, fromUserId, fromUserName, fromUserProfilePic, toUserId, toUserName, toUserProfilePic)





        //addChatMessageCount(currentUserId, fromUserId, fromUserName, fromUserProfilePic, toUserId, toUserName, toUserProfilePic)
        //addChatMessageCount(currentUserId, fromUserId, toUserId)
    }
});

connection.on("refreshOnlineUserByUserID", function (userID, isOnline, lastSeen) {
    var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
    if (currentChatUserID == userID) {
        if (isOnline == true) {
            $(document).find('span[class="spn-chat-user-online-status"]').html('<i class="fa fa-circle online-circle chat-user-online-status"></i>Online');
        }
        else {
            $(document).find('span[class="spn-chat-user-online-status"]').text('Last seen : ' + lastSeen + '');
        }
    }
});

function sendResponseToRequest(userid, requestResponse, loggedInUserID) {
    connection.invoke("sendResponseToRequest", userid, requestResponse, loggedInUserID);
}
function sendFriendRequest(userID, loggedInUserID) {
    connection.invoke("sendRequest", userID, loggedInUserID);

}

///
function sendNotificationByuser(userID, loggedInUserID, message) {
    connection.invoke("sendNotificationByuser", userID, loggedInUserID, message);

}
///
function refreshUserNotificationCounts(loggedInUserID) {
    connection.invoke("refreshNotificationCounts", loggedInUserID);
}
function changeUserNotificationStatus(notificationID) {
    connection.invoke("changeNotitficationStatus", notificationID, $('#hdfLoggedInUserID').val());
}
function refreshOnlineUsers() {
    $(document).find('.online-friends').load('/Home/_OnlineFriends', function () {
        var recentChats = $(document).find('.recent-chats').find('a');
        $(recentChats).each(function (cIndex, cItem) {
            changeUserOnlineStatus(cItem);
        });
        var friends = $('.user-friends');
        if (friends.length > 0) {
            var friendList = $(friends).find('li');
            console.log(friendList);
            $(friendList).each(function (cIndex, cItem) {
                changeUserOnlineStatus(cItem);
            });
        }
    });
}




function changeUserOnlineStatus(cItem) {
    $(cItem).find('img').removeClass('online-user-profile-pic');
    var userID = $(cItem).attr('data-user-id');
    var onlineItem = $(document).find('.online-friends').find('a[data-user-id="' + userID + '"]');
    if (onlineItem.length > 0) {
        $(cItem).find('img').addClass('online-user-profile-pic');
    }
}
function changeUserNotificationCounts(notificationCounts) {
    if (notificationCounts != null && notificationCounts != '' && notificationCounts != 0 && notificationCounts != '0') {
        $('.user-notification-count').text(notificationCounts);
    }
    else {
        $('.user-notification-count').text('');
    }
}
function removeHtmlElement(ele) {
    if (ele.length > 0) {
        ele.animate({ "opacity": "hide", top: "100" }, 500);
        setTimeout(function () {
            ele.remove();
        }, 500);
    }
}
function removeNotificationPop(notificationID) {
    var notificationPopup = $(document).find('#divNotificationPopUp-' + notificationID + '');
    removeHtmlElement(notificationPopup);
}
function unfriendUser(friendMappingID) {
    connection.server.unfriendUser(friendMappingID);
}
function initiateChat(toUserID) {
    $("#divBody").html('');
    $("#divBody").load('/Home/_Messages/' + toUserID, function () {
        $("#divBody").animate({ "opacity": "show", top: "100" }, 500);
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        UpdateChatMessageStatus(0, currentChatUserID);
        var recentChat = $(document).find('.recent-chats').find('a[data-user-id="' + toUserID + '"]');
        if (recentChat.length > 0) {
            var badge = $(recentChat).find('span');
            if ($(badge).hasClass('chat-message-count') && !$(badge).hasClass('hide')) {
                $(badge).text('');
                $(badge).addClass('hide');
            }
        }
    });
}
function createNewMessageBlockHtml(name, profilePicture, createOn, message, align, chatMessageID, status) {
    //var html = '<li class="' + align + '" data-chat-message-id="' + chatMessageID + '"><img src="' + profilePicture + '" alt="' + name + '" class="avatar"><span class="message"><span class="arrow"></span><span class="from">' + name + '</span>&nbsp;<span class="time">' + createOn + '</span>' + (align == 'right' ? '<span data-chat-message-id="' + chatMessageID + '" class="chat-message-status">' + status + '</span>' : '') + '<br /><span class="text">' + message + '</span></span></li>';
    var html = '<li class="' + align + '" data-chat-message-id="' + chatMessageID + '"> <div class="media chat-messages"><a class="media-left photo-table" href="#!"><img class="media-object img-radius img-radius m-t-5" src="' + profilePicture + '" alt="' + name + '">  </a><div class="media-body chat-menu-content"><div class=""> <p class="chat-cont">' + name + ':<br /> ' + message + ' <span data-chat-message-id=' + chatMessageID + ' class="chat-message-status">' + status + '</span> </div><p class="chat-time">' + createOn + '</p></div></div></li>';
    //var html = '<li class="' + align + '" data-chat-message-id="' + chatMessageID + '"> <div class="media chat-messages"><a class="media-left photo-table" href="#!"><img class="media-object img-radius img-radius m-t-5" src="' + profilePicture + '" alt="' + name + '">  </a><div class="media-body chat-menu-content"><div class=""> <p class="chat-cont">' + name + ':<br /> ' + message + '</p><p class="chat-cont"><span data-chat-message-id=' + chatMessageID + ' class="chat-message-status">' + status + '</span>' + message + '</p></div><p class="chat-time">' + createOn + '</p></div></div></li>';
    return html;
}
function sendChatMessage() {
    var fromUserID = $('#hdfLoggedInUserID').val();
    var fromUserName = $('#hdfLoggedInUserName').val();
    var fromUserPrifilePic = $('#hdfLoggedInUserProfilePicture').val();
    var chatMessage = $(document).find('.txt-chat-message').val();
    var toUserID = $(document).find('.hdf-current-chat-user-id').val();
    var toUserName = $(document).find('.hdf-current-chat-user-name').val();
    var toUserProfilePic = $(document).find('.hdf-current-chat-user-profile-picture').val();
    //if (chatMessage != null && chatMessage != '') {
    //	connection.invoke("SendMessage",fromUserID, toUserID, chatMessage, fromUserName, fromUserPrifilePic, toUserName, toUserProfilePic);
    //	$(document).find('.txt-chat-message').val('');
    //}

    if (chatMessage != null && chatMessage != '') {

        connection.invoke("SendMessage", fromUserID, toUserID, chatMessage, fromUserName, fromUserPrifilePic, toUserName, toUserProfilePic).catch(function (err) {
            return console.error(err.toString());
        });

        //connection.server.sendMessage(fromUserID, toUserID, chatMessage, fromUserName, fromUserPrifilePic, toUserName, toUserProfilePic);
        $(document).find('.txt-chat-message').val('');
    }
}
function createNewMessageBlock(name, profilePicture, createOn, message, align, chatMessageID, status) {
    $(document).find('ul.chat').append(createNewMessageBlockHtml(name, profilePicture, createOn, message, align, chatMessageID, status));
    $(document).find("div.right-chat-panel").animate({ scrollTop: $(document).find("div.right-chat-panel")[0].scrollHeight }, 500);
}
function sendUserTypingStatus() {
    var toUserID = $(document).find('.hdf-current-chat-user-id').val();
    var fromUserID = $('#hdfLoggedInUserID').val();
    //connection.server.sendUserTypingStatus(toUserID, fromUserID);

    connection.invoke("SendUserTypingStatus", toUserID, fromUserID).catch(function (err) {
        return console.error(err.toString());
    });
}
function refreshRecentChats() {
    $('.recent-chats').load('/Home/_RecentChats', function () {

    });
}
function addChatMessageCount(currentUserId, fromUserId, fromUserName, fromUserProfilePic, toUserId, toUserName, toUserProfilePic) {
    var recentChatWindow = $(document).find('.recent-chats');
    var recentChatItem = $(recentChatWindow).find('a[data-user-id="' + ((currentUserId != fromUserId) ? fromUserId : toUserId) + '"]');
    if (recentChatItem.length > 0) {
        $(recentChatItem).parent().prepend(recentChatItem);
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        if (currentUserId != fromUserId && (currentChatUserID != fromUserId)) {
            var messageCountItem = $(recentChatItem).find('span[data-user-id="' + fromUserId + '"]');
            var count = messageCountItem.text();
            if (count.match(/^\d+$/)) {
                $(messageCountItem).text(parseInt(count) + 1);
            }
            else {
                $(messageCountItem).removeClass('hide');
                $(messageCountItem).text(1);
            }

        } 
    }
    else {

        var html = '';
        if (currentUserId != fromUserId) {
            var uName = fromUserName.split(' ');
            html = '<a href="javascript:;" data-user-id="' + fromUserId + '" class="list-group-item chat-user"><img src="' + fromUserProfilePic + '" class="profilePictureCircle online-user-profile-pic" />&nbsp;&nbsp;&nbsp;' + uName + '<span class="custom-badge chat-message-count" data-user-id="' + fromUserId + '">1</span></a>';
            var messageCountItemnew = $('.receved-chat-message-count'); 
            var count = messageCountItemnew.text();
            if (count.match(/^\d+$/)) {
                $(messageCountItemnew).text(parseInt(count) + 1);
            }
            else {
                $(messageCountItemnew).removeClass('hide');
                $(messageCountItemnew).text(1);
            }
        }
        else {

            var uName = toUserName;//.split(' ');
            html = '<a href="javascript:;" data-user-id="' + toUserId + '" class="list-group-item chat-user"><img src="' + toUserProfilePic + '" class="profilePictureCircle online-user-profile-pic" />&nbsp;&nbsp;&nbsp;' + uName + '<span class="custom-badge chat-message-count hide" data-user-id="' + toUserId + '"></span></a>';
        }
        if ($('.no-recent-chats').length > 0) {
            $('.no-recent-chats').remove();
        }
        $(recentChatWindow).prepend(html);

    }
}


function UpdateChatMessageStatus(messageID, fromUserID) {
    var currentUserID = $('#hdfLoggedInUserID').val();
    //connection.server.updateMessageStatus(messageID, currentUserID, fromUserID);
    // connection.Invoke("UpdateMessageStatus", messageID, currentUserID, fromUserID);

    connection.invoke("UpdateMessageStatus", messageID, currentUserID, fromUserID).catch(function (err) {
        return console.error(err.toString());
    });


}
function GetOldMessages() {
    var isOldMessageExsit = $(document).find('.hdf-old-messages-exist');
    if ($(isOldMessageExsit).val() == "True") {
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        var lastMessageID = $(document).find('.hdf-last-chat-message-id').val();
        console.log($(document).find("div.right-chat-panel").scrollTop())
        $.get('/Chat/GetRecentMessages?Id=' + currentChatUserID + '&lastChatMessageId=' + lastMessageID, function (messages) {
            if (messages.ChatMessages.length > 0) {
                $(isOldMessageExsit).val((messages.ChatMessages.length < 20 ? "False" : "True"));
                $(document).find('.hdf-last-chat-message-id').val(messages.LastChatMessageId);
                var html = '';
                var currentUserId = $('#hdfLoggedInUserID').val();
                var fromUserName = $('#hdfLoggedInUserName').val();
                var fromUserPrifilePic = $('#hdfLoggedInUserProfilePicture').val();
                var chatMessage = $(document).find('.txt-chat-message').val();
                var toUserID = $(document).find('.hdf-current-chat-user-id').val();
                var toUserName = $(document).find('.hdf-current-chat-user-name').val();
                var toUserProfilePic = $(document).find('.hdf-current-chat-user-profile-picture').val();
                $(messages.ChatMessages).each(function (index, item) {
                    if (item.FromUserID == currentUserId) {
                        html += createNewMessageBlockHtml(fromUserName, fromUserPrifilePic, item.CreatedOn, item.Message, "right", item.ChatMessageID, item.Status);
                    }
                    else {
                        html += createNewMessageBlockHtml(toUserName, toUserProfilePic, item.CreatedOn, item.Message, "left", item.ChatMessageID, item.Status);
                    }
                });
                var firstMsg = $('ul.chat li:first');
                $(document).find('ul.chat').prepend(html);
                $(document).find("div.right-chat-panel").scrollTop(firstMsg.offset().top);
            }
            else {
                $(isOldMessageExsit).val("False");
            }
        });
    }
}
