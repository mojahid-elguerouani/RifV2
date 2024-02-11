using ChatApp.Domain.Abstract;
using ChatApp.Domain.Concrete;
using ChatApp.Domain.Entity;
using FasDemo.Data;
using FasDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Concrete
{
    public class EFUserRepository : IUser
    {
        private readonly ApplicationDbContext _context;

        public EFUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tuple<ApplicationUser, string> SaveUser(ApplicationUser objentity)
        {
            var obj = _context.ApplicationUser.Where(m => m.Id == objentity.Id).FirstOrDefault();
            if (obj != null)
            {
                obj.UserName = objentity.UserName;
                //obj.Gender = objentity.Gender;
                //obj.DOB = objentity.DOB;
                //obj.Bio = objentity.Bio;
                //obj.UpdatedOn = System.DateTime.Now;
            }
            else
            {
                var existUserName = _context.Users.Where(m => m.UserName == objentity.UserName).FirstOrDefault();
                if (existUserName != null)
                {
                    return new Tuple<ApplicationUser, string>(objentity, "User name is already exist. Please try with another user name.");
                }
                _context.Users.Add(objentity);
            }
            _context.SaveChanges();
            return new Tuple<ApplicationUser, string>(objentity, "");
        }
        public ApplicationUser CheckLogin(string userName, string password)
        {
            var obj = _context.ApplicationUser.Where(m => m.UserName == userName && m.PasswordHash == password).FirstOrDefault();
            return obj;
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
        public List<string> GetUserConnectionID(string[] userIDs)
        {
            var obj = _context.OnlineUsers.Where(m => userIDs.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
            return obj;
        }
        public List<ApplicationUser> GetAllUsers()
        {
            var objList = _context.ApplicationUser.ToList();
            return objList;
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
                           Name = v.UserName,
                           //ProfilePicture = v.ProfilePicture,
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
        public ApplicationUser GetUserById(string userId)
        {
            var obj = _context.ApplicationUser.Where(m => m.Id == userId).FirstOrDefault();
            return obj;
        }
        public string[] GetFriendUserIds(string userID)
        {
            var arr = _context.FriendMappings.Where(m => (m.RequestorUserID == userID || m.EndUserID == userID) && m.RequestStatus == "Accepted" && m.IsActive == true).Select(m => m.RequestorUserID == userID ? m.EndUserID : m.RequestorUserID).ToArray();
            return arr;
        }
        public List<FriendRequests> GetSentFriendRequests(string userID)
        {
            var list = (from u in _context.FriendMappings
                        join v in _context.ApplicationUser on u.EndUserID.ToString() equals v.Id
                        where u.RequestorUserID.ToString() == userID && u.RequestStatus == "Sent" && u.IsActive == true
                        select new FriendRequests()
                        {
                            UserInfo = v,
                            RequestStatus = u.RequestStatus,
                            EndUserID = u.EndUserID,
                            RequestorUserID = u.RequestorUserID
                        }).ToList();
            return list;
        }
        public List<FriendRequests> GetReceivedFriendRequests(string userID)
        {
            var list = (from u in _context.FriendMappings
                        join v in _context.ApplicationUser on u.RequestorUserID equals v.Id
                        where u.EndUserID == userID && u.RequestStatus == "Sent" && u.IsActive == true
                        select new FriendRequests()
                        {
                            UserInfo = v,
                            RequestStatus = u.RequestStatus,
                            EndUserID = u.EndUserID,
                            RequestorUserID = u.RequestorUserID
                        }).ToList();
            return list;
        }

        public List<FriendRequests> GetAllSentFriendRequests()
        {
            var list = (from u in _context.FriendMappings
                        join v in _context.ApplicationUser on u.EndUserID.ToString() equals v.Id
                        where u.RequestStatus == "Sent" && u.IsActive == true
                        select new FriendRequests()
                        {
                            UserInfo = v,
                            RequestStatus = u.RequestStatus,
                            EndUserID = u.EndUserID,
                            RequestorUserID = u.RequestorUserID
                        }).ToList();
            return list;
        }
        public List<UserSearchResult> SearchUsers(string name, string userID)
        {
            string[] friendIds = GetFriendUserIds(userID);
            var objList = _context.ApplicationUser.Where(m => m.NormalizedUserName.ToLower().Contains(name.ToLower()) && m.Id != userID && !friendIds.Contains(m.Id)).ToList();
            var receivedRequests = GetReceivedFriendRequests(userID);
            var sentRequests = GetSentFriendRequests(userID);
            List<UserSearchResult> list = new List<UserSearchResult>();
            foreach (var item in objList)
            {
                bool isReceived = false;
                var receivedRequest = receivedRequests.Where(x => x.UserInfo.Id == item.Id).FirstOrDefault();
                if (receivedRequest != null)
                {
                    isReceived = true;
                }
                var userInfo = new UserSearchResult();
                userInfo.IsRequestReceived = isReceived;
                userInfo.UserInfo = item;
                var sentRequest = sentRequests.Where(m => m.UserInfo.Id == item.Id).FirstOrDefault();
                if (sentRequest != null)
                {
                    userInfo.FriendRequestStatus = sentRequest.RequestStatus; ;
                }
                list.Add(userInfo);
            }
            return list;
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
        public FriendMapping GetFriendRequestStatus(string userID)
        {
            var obj = _context.FriendMappings.Where(m => (m.EndUserID == userID || m.RequestorUserID == userID) && m.IsActive == true).FirstOrDefault();
            return obj;
        }
        public int ResponseToFriendRequest(string requestorID, string requestResponse, string endUserID)
        {
            var request = _context.FriendMappings.Where(m => m.EndUserID == endUserID && m.RequestorUserID == requestorID && m.IsActive == true).FirstOrDefault();
            if (request != null)
            {
                request.RequestStatus = requestResponse;
                request.UpdatedOn = System.DateTime.Now;
                _context.SaveChanges();
            }
            var notification = _context.UserNotifications.Where(m => m.ToUserID == endUserID && m.FromUserID == requestorID && m.IsActive == true && m.NotificationType == "FriendRequest").FirstOrDefault();
            if (notification != null)
            {
                notification.IsActive = false;
                notification.UpdatedOn = System.DateTime.Now;
                _context.SaveChanges();
                return notification.NotificationID;
            }
            return 0;
        }
        public List<UserNotificationList> GetUserNotifications(string toUserID)
        {
            var listQuery = (from u in _context.UserNotifications
                             join v in _context.ApplicationUser on u.FromUserID equals v.Id
                             where u.ToUserID == toUserID && u.IsActive == true
                             select new UserNotificationList()
                             {
                                 NotificationID = u.NotificationID,
                                 NotificationType = u.NotificationType,
                                 User = v,
                                 NotificationStatus = u.Status,
                                 CreatedOn = u.CreatedOn
                             }).OrderByDescending(m => m.CreatedOn);
            int totalNotifications = listQuery.Count();
            var list = listQuery.Take(3).ToList();
            list.ForEach(m => m.TotalNotifications = totalNotifications);
            return list;
        }
        public int GetUserNotificationCounts(string toUserID)
        {
            int count = _context.UserNotifications.Where(m => m.Status == "New" && m.ToUserID == toUserID && m.IsActive == true).Count();
            return count;
        }
        public void ChangeNotificationStatus(int[] notificationIDs)
        {
            _context.UserNotifications.Where(m => notificationIDs.Contains(m.NotificationID)).ToList().ForEach(m => m.Status = "Read");
            _context.SaveChanges();
        }
        public FriendMapping RemoveFriendMapping(int friendMappingID)
        {
            var obj = _context.FriendMappings.Where(m => m.FriendMappingID == friendMappingID).FirstOrDefault();
            if (obj != null)
            {
                obj.IsActive = false;
                _context.SaveChanges();
            }
            return obj;
        }
        public void UpdateProfilePicture(string userID, string profilePicturePath)
        {

        }
        public List<ApplicationUser> GetUsersByLinqQuery(Expression<Func<ApplicationUser, bool>> where)
        {
            var objList = _context.ApplicationUser.Where(where).ToList();
            return objList;
        }
        public List<OnlineUserDetails> GetRecentChats(string currentUserID)
        {
            string[] friends = GetFriendUserIds(currentUserID);
            var recentMessages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserID == currentUserID || m.FromUserID == currentUserID)).OrderByDescending(m => m.CreatedOn).ToList();
            var userIds = recentMessages.Select(m => (m.ToUserID == currentUserID ? m.FromUserID : m.ToUserID)).Distinct().ToArray();
            var userIdsList = userIds.ToList();
            var messagesByUserId = recentMessages.Where(m => m.ToUserID == currentUserID && m.Status == "Sent").ToList();
            var newMessagesCount = (from p in messagesByUserId
                                    group p by p.FromUserID into g
                                    select new { FromUserID = g.Key, Count = g.Count() }).ToList();
            var onlineUserIDs = _context.OnlineUsers.Where(m => friends.Contains(m.UserID) && userIds.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserID).ToArray();
            var users = (from m in _context.Users
                         join v in userIdsList on m.Id equals v
                         select new OnlineUserDetails
                         {
                             UserID = m.Id,
                             Name = m.UserName,
                             //ProfilePicture = m.ProfilePicture,
                             //Gender = m.Gender,
                             IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
                         }).ToList();
            users.ForEach(m =>
            {
                m.UnReadMessageCount = newMessagesCount.Where(x => x.FromUserID == m.UserID).Select(x => x.Count).FirstOrDefault();
            });
            users = users.OrderBy(d => userIdsList.IndexOf(d.UserID)).ToList();
            return users;
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
        public void UpdateUserProfilePicture(string userID, string imagePath)
        {
            var obj = _context.Users.Where(m => m.Id == userID).FirstOrDefault();
            if (obj != null)
            {
                //obj.ProfilePicture = imagePath;
                //obj.UpdatedOn = System.DateTime.Now;
                _context.SaveChanges();
                SaveUserImage(userID, imagePath, true);
            }
        }
        public void SaveUserImage(string userID, string imagePath, bool isProfilePicture)
        {
            if (isProfilePicture)
            {
                var existingProfilePicture = _context.UserImages.Where(m => m.UserID == userID && m.IsActive == true && m.IsProfilePicture == true).FirstOrDefault();
                if (existingProfilePicture != null)
                {
                    existingProfilePicture.IsProfilePicture = false;
                    _context.SaveChanges();
                }
            }
            UserImage objentity = new UserImage();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.ImagePath = imagePath;
            objentity.IsActive = true;
            objentity.IsProfilePicture = isProfilePicture;
            objentity.UserID = userID;
            _context.UserImages.Add(objentity);
            _context.SaveChanges();
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
    }
}
