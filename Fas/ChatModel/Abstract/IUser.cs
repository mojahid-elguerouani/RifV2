using ChatApp.Domain.Entity;
using FasDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Abstract
{
    public interface IUser
    {
        Tuple<ApplicationUser, string> SaveUser(ApplicationUser objentity);
        ApplicationUser CheckLogin(string userName, string password);
        void SaveUserOnlineStatus(OnlineUser objentity);
        List<string> GetUserConnectionID(string UserID);
        List<string> GetUserConnectionID(string[] userIDs);
        List<ApplicationUser> GetAllUsers();
        List<OnlineUserDetails> GetOnlineFriends(string userID);
        ApplicationUser GetUserById(string userId);
        List<UserSearchResult> SearchUsers(string name, string userID);
        List<FriendRequests> GetSentFriendRequests(string userID);
        List<FriendRequests> GetReceivedFriendRequests(string userID);
        void SendFriendRequest(string endUserID, string loggedInUserID);
        int SaveUserNotification(string notificationType, string fromUserID, string toUserID);
        FriendMapping GetFriendRequestStatus(string userID);
        int ResponseToFriendRequest(string requestorID, string requestResponse, string endUserID);
        List<UserNotificationList> GetUserNotifications(string toUserID);
        int GetUserNotificationCounts(string toUserID);
        void ChangeNotificationStatus(int[] notificationIDs);
        FriendMapping RemoveFriendMapping(int friendMappingID);
        List<ApplicationUser> GetUsersByLinqQuery(Expression<Func<ApplicationUser, bool>> where);
        List<OnlineUserDetails> GetRecentChats(string currentUserID);
        OnlineUserDetails GetUserOnlineStatus(string userID);
        void UpdateUserProfilePicture(string userID, string imagePath);
        void SaveUserImage(string userID, string imagePath, bool isProfilePicture);
        List<OnlineUserDetails> GetFriends(string userID);
    }
}
