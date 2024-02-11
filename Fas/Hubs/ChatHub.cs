using ChatApp;
using ChatApp.Domain.Concrete;
using ChatApp.Domain.Entity;
using ChatApp.Models;
using FasDemo.Data;
using FasDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.Hubs
{
    public class ChatHub : Hub
    {
        //public readonly EFUserRepository _UserRepo;
        //EFMessageRepository _MessageRepo = new EFMessageRepository();

        public readonly ApplicationDbContext _context;
        //public readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task SendMessage(string user, string message)
        //{ 
        //    await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message); 
        //}




        //public async Task SendMessgage(Message message) =>
        //    await Clients.All.SendAsync("reciveMessages", message);
        //public async Task SendMessage(string user, string message)
        //{
        //    var userID = Context.User.Identity.Name;
        //    var ConnectionId = Context.ConnectionId;
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        //public async Task Send(string message)
        //{
        //    await Clients.All.SendAsync("SendMessage", Context.User.Identity.Name, message);
        //}
        public override async Task OnConnectedAsync()
        {
            try
            {
                await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "joined");

                var a = Context.User.Identities.ToArray();
                var b = a[0].Claims.ToArray();
                var userID = b[0].Value;//  Context.User.Identity.Name;
                var ConnectionId = Context.ConnectionId;

                if (userID != null)
                {
                    SaveUserOnlineStatus(new OnlineUser { UserID = userID, ConnectionID = Context.ConnectionId, IsOnline = true });
                    RefreshOnlineUsers(userID);
                }
                // return await base.OnConnectedAsync();

            }
            catch (Exception ex)
            {
                // Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            // return await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "left");
            var a = Context.User.Identities.ToArray();
            var b = a[0].Claims.ToArray();
            var userID = b[0].Value;//  Context.User.Identity.Name; 
            if (userID != null)
            {
                SaveUserOnlineStatus(new OnlineUser { UserID = userID, ConnectionID = Context.ConnectionId, IsOnline = false });
                RefreshOnlineUsers(userID);
            }
            //return base.OnDisconnected(stopCalled);

            return base.OnDisconnectedAsync(exception);
        }

        public void SendRequest(string userID, string loggedInUserID)
        {
            SendFriendRequest(userID, loggedInUserID);
            SendNotification(loggedInUserID, userID, "FriendRequest");
        }
        public void SendNotificationByuser(string userID, string loggedInUserID, string msg)
        {
            SendFriendRequest(userID, loggedInUserID);
            SendNotificationUpdate(loggedInUserID, userID, msg);
        }
        public void SendNotificationUpdate(string fromUserID, string toUserID, string notificationType)
        {
            int notificationID = SaveUserNotification(notificationType, fromUserID, toUserID);
            var connectionId = GetUserConnectionID(toUserID);
            if (connectionId != null && connectionId.Count() > 0)
            {
                var userInfo = GetUserModel(fromUserID);
                int notificationCounts = GetUserNotificationCounts(toUserID);
                Clients.Clients(connectionId).SendAsync("ReceiveNotificationUpdate", notificationType, userInfo.ProfilePicture,userInfo.Name, notificationID, notificationCounts);
            }
        }
        public void SendNotification(string fromUserID, string toUserID, string notificationType)
        {
            int notificationID = SaveUserNotification(notificationType, fromUserID, toUserID);
            var connectionId = GetUserConnectionID(toUserID);
            if (connectionId != null && connectionId.Count() > 0)
            {
                var userInfo = GetUserModel(fromUserID);
                int notificationCounts = GetUserNotificationCounts(toUserID);
                Clients.Clients(connectionId).SendAsync("ReceiveNotification", notificationType, userInfo, notificationID, notificationCounts);
            }
        }


        public void RefreshNotificationCounts(string toUserID)
        {
            var connectionId = GetUserConnectionID(toUserID);
            if (connectionId != null && connectionId.Count() > 0)
            {
                int notificationCounts = GetUserNotificationCounts(toUserID);
                Clients.Clients(connectionId).SendAsync("RefreshNotificationCounts", notificationCounts);
            }
        }
        public void ChangeNotitficationStatus(string notificationIds, string toUserID)
        {
            if (!string.IsNullOrEmpty(notificationIds))
            {
                string[] arrNotificationIds = notificationIds.Split(',');
                int[] ids = arrNotificationIds.Select(m => Convert.ToInt32(m)).ToArray();
                ChangeNotificationStatus(ids);
                RefreshNotificationCounts(toUserID);
            }
        }
        public void ChangeNotificationStatus(int[] notificationIDs)
        {
            _context.UserNotifications.Where(m => notificationIDs.Contains(m.NotificationID)).ToList().ForEach(m => m.Status = "Read");
            _context.SaveChanges();
        }

        public UserModel GetUserModel(string id, ApplicationUser objentity = null, string friendRequestStatus = "", bool isRequestReceived = false)
        {
            var user = new ApplicationUser();
            if (objentity != null)
            {
                user = objentity;
            }
            else
            {

                user = GetUserById(id);
            }
            UserModel objmodel = new UserModel();
            if (user != null)
            {
                objmodel.IsRequestReceived = isRequestReceived;
                objmodel.FriendRequestStatus = friendRequestStatus;
                objmodel.UserID = user.Id;
                objmodel.Name = GetProfileName(user.Id);
                objmodel.ProfilePicture = GetProfilePicture(user.Id);
               // objmodel.ProfilePicture = CommonFunctions.GetProfilePicture(user.ProfilePicture, user.Gender);
                //objmodel.Gender = user.Gender;
                //objmodel.DOB = user.DOB.ToShortDateString();
                //if (user.DOB != null)
                //{
                //    objmodel.Age = Convert.ToString(Math.Floor(DateTime.Now.Subtract(Convert.ToDateTime(user.DOB)).TotalDays / 365.0)) + " Years";
                //}
                //else
                //{
                //    objmodel.Age = "NaN";
                //}
                //objmodel.Bio = user.Bio;
            }
            return objmodel;
        }

        public ApplicationUser GetUserById(string userId)
        {
            var obj = _context.ApplicationUser.Where(m => m.Id == userId).FirstOrDefault();
            return obj;
        }


        public void SendFriendRequest(string endUserID, string loggedInUserID)
        {
            FriendMapping objentity = new FriendMapping();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.EndUserID = endUserID;
            objentity.IsActive = true;
            objentity.RequestorUserID = loggedInUserID;
            objentity.RequestStatus = "Sent";
            objentity.UpdatedOn = System.DateTime.Now;
            _context.FriendMappings.Add(objentity);
            _context.SaveChanges();
        }
        public int SaveUserNotification(string notificationType, string fromUserID, string toUserID)
        {
            UserNotification notification = new UserNotification();
            notification.CreatedOn = System.DateTime.Now;
            notification.IsActive = true;
            notification.NotificationType = notificationType;
            notification.FromUserID = fromUserID;
            notification.Status = "New";
            notification.UpdatedOn = System.DateTime.Now;
            notification.ToUserID = toUserID;
            _context.UserNotifications.Add(notification);
            _context.SaveChanges();
            return notification.NotificationID;
        }
        public int GetUserNotificationCounts(string toUserID)
        {
            int count = _context.UserNotifications.Where(m => m.Status == "New" && m.ToUserID == toUserID && m.IsActive == true).Count();
            return count;
        }


        //public void RefreshOnlineUsers(string userID)
        //{
        //    var users = _UserRepo.GetOnlineFriends(userID);
        //    RefreshOnlineUsersByConnectionIds(users.SelectMany(m => m.ConnectionID).ToList(), userID);
        //}
        //public void RefreshOnlineUsersByConnectionIds(List<string> connectionIds, string userID = "0")
        //{
        //    //await Clients.Clients(connectionIds).RefreshOnlineUsers();
        //    //if (userID != "0")
        //    //{
        //    //    var onlineStatus = _UserRepo.GetUserOnlineStatus(userID);
        //    //    if (onlineStatus != null)
        //    //    {
        //    //        await Clients.Clients(connectionIds).RefreshOnlineUserByUserID(userID, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
        //    //    }
        //    //}
        //}


        public async Task SendMessage(string fromuserid, string touserid, string message, string fromUserName, string fromUserProfilePic, string toUserName, string toUserProfilePic)
        {
            var userid = Context.User.Identity.Name;
            var connectionid = Context.ConnectionId;
            //await Clients.All.SendAsync("receivemessage", fromuserid, message);

            ChatMessage objentity = new ChatMessage();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.FromUserID = fromuserid;
            objentity.IsActive = true;
            objentity.Message = message;
            objentity.ViewedOn = System.DateTime.Now;
            objentity.Status = "sent";
            objentity.ToUserID = touserid;
            objentity.UpdatedOn = System.DateTime.Now;
            //var obj = _messagerepo.savechatmessage(objentity);
            _context.ChatMessages.Add(objentity);
            _context.SaveChanges();
            var messagerow = CommonFunctions.GetMessageModel(objentity);
            List<string> connectionids = GetUserConnectionIDs(new string[] { fromuserid, touserid });
            await Clients.Clients(connectionids).SendAsync("AddNewChatMessage", messagerow, fromuserid, touserid, fromUserName, fromUserProfilePic, toUserName, toUserProfilePic);
            //await Clients.Clients(connectionids).SendAsync("newmessage", messagerow);
            //await Clients.Caller.SendAsync("newmessage", messagerow);
        }



        public async void SendUserTypingStatus(string toUserID, string fromUserID)
        {
            List<string> connectionIds = GetUserConnectionIDs(new string[] { toUserID });
            if (connectionIds.Count > 0)
            {
                await Clients.Clients(connectionIds).SendAsync("UserIsTyping", fromUserID);
                await Clients.All.SendAsync("ReceiveTyper", fromUserID, "isTyping");
            }
        }
        public void UpdateMessageStatus(int messageID, string currentUserID, string fromUserID)
        {
            if (messageID > 0)
            {
                UpdateMessageStatusByMessageID(messageID);
            }
            else
            {
                UpdateMessageStatusByUserID(fromUserID, currentUserID);
            }
            List<string> connectionIds = GetUserConnectionIDs(new string[] { currentUserID, fromUserID });
            //Clients.Clients(connectionIds).UpdateMessageStatusInChatWindow(messageID, currentUserID, fromUserID);
        }



        public void RefreshOnlineUsers(string userID)
        {
            var users = GetOnlineFriends(userID);
            RefreshOnlineUsersByConnectionIds(users.SelectMany(m => m.ConnectionID).ToList(), userID);
        }
        public void RefreshOnlineUsersByConnectionIds(List<string> connectionIds, string userID = "0")
        {

            Clients.Clients(connectionIds).SendAsync("refreshOnlineUsers");
            //Clients.Clients(connectionIds).RefreshOnlineUsers();
            if (userID != "0")
            {
                var onlineStatus = GetUserOnlineStatus(userID);
                if (onlineStatus != null)
                {
                    Clients.Clients(connectionIds).SendAsync("RefreshOnlineUserByUserID", userID, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
                    //Clients.Clients(connectionIds).RefreshOnlineUserByUserID(userID, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
                }
            }
        }



        public List<OnlineUserDetails> GetOnlineFriends(string userID)
        {
            string[] friends = GetFriendUserIds(userID);
            var friendOnlineDetails = _context.OnlineUsers.Where(m => friends.Contains(m.UserID) && m.IsOnline == true).ToList();
            var obj = (from v in _context.Users
                       where friends.Contains(v.Id)
                       select new OnlineUserDetails
                       {
                           UserID = v.Id,
                           Name = GetProfileName(userID),
                           ProfilePicture = GetProfilePicture(userID),
                           //Gender = v.Gender
                       }).OrderBy(m => m.Name).ToList();
            var onlineUserIds = friendOnlineDetails.Select(m => m.UserID).ToArray();
            obj = obj.Where(m => onlineUserIds.Contains(m.UserID)).ToList();
            obj.ForEach(m =>
            {
                m.ConnectionID = friendOnlineDetails.Where(x => x.UserID == m.UserID).Select(x => x.ConnectionID).ToList();
            });
            return obj;
        }

        public OnlineUserDetails GetUserOnlineStatus(string userID)
        {
            OnlineUserDetails obj = new OnlineUserDetails();
            obj.UserID = userID;
            var objList = _context.OnlineUsers.Where(m => m.UserID == userID && m.IsActive == true).ToList();
            if (objList != null && objList.Count > 0)
            {
                obj.IsOnline = false;
                var onlineConnections = objList.Where(m => m.IsOnline).ToList();
                var offlineConnections = objList.Where(m => !m.IsOnline).ToList();
                if (onlineConnections != null && onlineConnections.Count > 0)
                {
                    obj.IsOnline = true;
                }
                else if (offlineConnections != null && offlineConnections.Count > 0)
                {
                    obj.IsOnline = false;
                    obj.LastUpdationTime = offlineConnections.OrderByDescending(m => m.UpdatedOn).Select(m => m.UpdatedOn).FirstOrDefault();
                }
            }
            return obj;
        }
        public List<OnlineUserDetails> GetFriends(string userID)
        {
            var friendIds = GetFriendUserIds(userID);
            var onlineUserIDs = _context.OnlineUsers.Where(m => friendIds.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserID).ToArray();
            var users = _context.Users.Where(m => friendIds.Contains(m.Id)).Select(m => new OnlineUserDetails
            {
                UserID = m.Id,
                Name = m.UserName,
                //ProfilePicture = m.ProfilePicture,
                //Gender = m.Gender,
                IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
            }).ToList();
            return users;
        }
        public string[] GetFriendUserIds(string userID)
        {
            var arr = _context.FriendMappings.Where(m => (m.RequestorUserID == userID || m.EndUserID == userID) && m.RequestStatus == "Accepted" && m.IsActive == true).Select(m => m.RequestorUserID == userID ? m.EndUserID : m.RequestorUserID).ToArray();
            return arr;
        }
        public void SaveUserOnlineStatus(OnlineUser objentity)
        {
            var obj = _context.OnlineUsers.Where(m => m.UserID == objentity.UserID && m.ConnectionID == objentity.ConnectionID).FirstOrDefault();
            if (obj != null)
            {
                obj.IsOnline = objentity.IsOnline;
                obj.UpdatedOn = System.DateTime.Now;
                obj.ConnectionID = objentity.ConnectionID;
            }
            else
            {
                objentity.CreatedOn = System.DateTime.Now;
                objentity.UpdatedOn = System.DateTime.Now;
                objentity.IsActive = true;
                _context.OnlineUsers.Add(objentity);
            }
            _context.SaveChanges();
        }
        public List<string> GetUserConnectionID(string UserID)
        {
            var obj = _context.OnlineUsers.Where(m => m.UserID == UserID && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
            return obj;
        }
        public List<string> GetUserConnectionIDs(string[] userIDs)
        {
            var obj = _context.OnlineUsers.Where(m => userIDs.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
            return obj;
        }
        public void UpdateMessageStatusByUserID(string fromUserID, string currentUserID)
        {
            var unreadMessages = _context.ChatMessages.Where(m => m.Status == "Sent" && m.ToUserID == currentUserID && m.FromUserID == fromUserID && m.IsActive == true).ToList();
            unreadMessages.ForEach(m =>
            {
                m.Status = "Viewed";
                m.ViewedOn = System.DateTime.Now;
            });
            _context.SaveChanges();
        }
        public void UpdateMessageStatusByMessageID(int messageID)
        {
            var unreadMessages = _context.ChatMessages.Where(m => m.ChatMessageID == messageID).FirstOrDefault();
            if (unreadMessages != null)
            {
                unreadMessages.Status = "Viewed";
                unreadMessages.ViewedOn = System.DateTime.Now;
                _context.SaveChanges();
            }
        }

        public string GetProfilePicture(string userId)
        {
            string profilePicturePath;
            profilePicturePath = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault() != null ?
                                               "/" + _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().ProfilePicture :
                                               "/assets/images/user/avatar-2.jpg";

            return profilePicturePath;
        }
        public string GetProfileName(string userId)
        {
            string profileProfileName;
            if (_context.Employee.Find(userId) == null)
                profileProfileName = "Super admin";
            else
                profileProfileName = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().FirstName;
            return profileProfileName;
        }


    }
}
