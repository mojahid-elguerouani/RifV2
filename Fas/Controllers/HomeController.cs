using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChatApp;
using ChatApp.Domain.Concrete;
using ChatApp.Domain.Entity;
using ChatApp.Domain.Entity.DTO;
using ChatApp.Models;
using FasDemo.Data;
using FasDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FasDemo.Controllers
{
    //[Authorize(Roles = Services.App.Pages.Home.RoleName)]
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currenntUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currenntUser.UserName;
            }

            return View();
        }


        //[HttpPost]
        //public ActionResult getusers()
        //{

        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Profile(int Id = 0)
        {
            if (Id == 0)
            {

                Id = 0;// MySession.Current.UserID;
            }
            var objmodel = CommonFunctions.GetUserModel(Id.ToString());
            if (Id != 0)/*MySession.Current.UserID*/
            {
                var friendInfo = GetFriendRequestStatus(Id.ToString());
                if (friendInfo != null)
                {
                    objmodel.FriendRequestStatus = friendInfo.RequestStatus;
                    objmodel.FriendEndUserID = friendInfo.EndUserID;
                    objmodel.FriendRequestorID = friendInfo.RequestorUserID;
                    objmodel.FriendMappingID = friendInfo.FriendMappingID;
                }
            }
            return View(objmodel);
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            var objmodel = CommonFunctions.GetUserModel("0");//MySession.Current.UserID
            return View(objmodel);
        }
        [HttpPost]
        public ActionResult EditProfile(UserModel objmodel)
        {
            var objentity = GetUserById("0");//MySession.Current.UserID
            objentity.FirstName = objmodel.Name;
            //objentity.Gender = objmodel.Gender;
            //objentity.DOB = Convert.ToDateTime(objmodel.DOB);
            //objentity.Bio = objmodel.Bio;
            //objentity.UpdatedOn = System.DateTime.Now;
            // var result = SaveUser(objentity);
            return RedirectToAction("Profile");
        }

        public ActionResult _UserSearchResult(string name)
        {
            var userList = SearchUsers(name, IdentityId());/*MySession.Current.UserID*/
            var objmodel = userList.Select(m => CommonFunctions.GetUserModel(m.UserInfo.Id, m.UserInfo, m.FriendRequestStatus, m.IsRequestReceived)).ToList();
            return PartialView(objmodel);
        }

        private string IdentityName
        {
            get { return User.Identity.Name; }
        }

        public ActionResult _OnlineFriends()
        {
            string CurrentUserID = _userManager.GetUserId(User);
            var onlineFriends = GetOnlineFriends(IdentityId());//MySession.Current.UserID
            var objmodel = onlineFriends.Select(m => new UserModel()
            {
                UserID = m.UserID,
                Name = userFirstName(m.UserID),
                ProfilePicture = userImage(m.UserID)// CommonFunctions.GetProfilePicture(m.ProfilePicture, m.Gender)
            }).ToList();
            return PartialView(objmodel);
        }
        public IActionResult _UserNotifications()
        {
            string CurrentUserID = _userManager.GetUserId(User);
            var notifications = GetUserNotifications(CurrentUserID);// ("928260f2-87fe-4a98-9a57-7885f668739f");//MySession.Current.UserID
            var objmodel = notifications.Select(m => new UserNotificationModel()
            {
                NotificationID = m.NotificationID,
                NotificationType = m.NotificationType,
                ApplicationUser = CommonFunctions.GetUserModel("0", m.User),
                NotificationStatus = m.NotificationStatus,
                CreatedOn = m.CreatedOn,
                TotalNotifications = m.TotalNotifications
            }).ToList();
            return PartialView(objmodel);
        }
        public ActionResult _RecentChats()
        {
            string CurrentUserID = _userManager.GetUserId(User);
            var recentChats = GetRecentChats(CurrentUserID);//MySession.Current.UserID
            var objmodel = recentChats.Select(m => new UserModel()
            {
                UserID = m.UserID,
                Name = userFirstName(m.UserID),
                ProfilePicture = userImage(m.UserID),// CommonFunctions.GetProfilePicture(m.ProfilePicture, m.Gender),
                IsOnline = m.IsOnline,
                UnReadMessages = m.UnReadMessageCount > 0 ? Convert.ToString(m.UnReadMessageCount) : ""
            }).ToList();
            return PartialView(objmodel);
        }
        [HttpPost]
        //public ActionResult UpdateProfilePicture(HttpPostedFileBase profilePicture, int userID)
        //{
        //    try
        //    {
        //        string filePath = string.Empty;
        //        if (profilePicture != null)
        //        {
        //            string folderpath = Server.MapPath("~/") + "Content/Images";
        //            if (!System.IO.Directory.Exists(folderpath))
        //            {
        //                System.IO.Directory.CreateDirectory(folderpath);
        //            }
        //            string path = Server.MapPath("~/Content/Images/ProfilePictures");
        //            if (!System.IO.Directory.Exists(path))
        //            {
        //                System.IO.Directory.CreateDirectory(path);
        //            }
        //            Random r = new Random();
        //            int randomNo = r.Next();
        //            filePath = "/Content/Images/ProfilePictures/" + randomNo + "_" + profilePicture.FileName;
        //            profilePicture.SaveAs(Server.MapPath("~/Content/Images/ProfilePictures/" + randomNo + "_" + profilePicture.FileName));
        //            _UserRepo.UpdateUserProfilePicture(userID, filePath);
        //            MySession.Current.ProfilePicture = filePath;
        //            return Json(new { success = true, filePath = filePath }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new { success = false, filePath = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, filePath = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult Friends()
        {
            string CurrentUserID = _userManager.GetUserId(User);
            var friendUsers = GetFriends(CurrentUserID);//MySession.Current.UserID
            var objmodel = friendUsers.Select(m => new UserModel()
            {
                UserID = m.UserID,
                Name = userFirstName(m.UserID),
                ProfilePicture = userImage(m.UserID),

                //CommonFunctions.GetProfilePicture(m.ProfilePicture, m.Gender),
                IsOnline = m.IsOnline,
            }).ToList();
            return View(objmodel);
        }


        public ActionResult _Messages(string Id)
        {
            var userModel = GetUserModel(Id);
            var messages = GetChatMessagesByUserID(IdentityId(), Id);
            var objmodel = new ChatMessageModel();
            objmodel.UserDetail = userModel;
            objmodel.ChatMessages = messages.Messages.Select(m => CommonFunctions.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            var onlineStatus = GetUserOnlineStatus(Id);
            if (onlineStatus != null)
            {
                objmodel.IsOnline = onlineStatus.IsOnline;
                objmodel.LastSeen = Convert.ToString(onlineStatus.LastUpdationTime);
            }
            return View(objmodel);
        }
        public ActionResult GetRecentMessages(string Id, int lastChatMessageId)
        {
            var messages = GetChatMessagesByUserID(IdentityId(), Id, lastChatMessageId);
            var objmodel = new ChatMessageModel();
            objmodel.ChatMessages = messages.Messages.Select(m => CommonFunctions.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            return Json(objmodel);
        }



        #region DDl  

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
            // string ProfPicture = userImage(userID);

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
                           ProfilePicture = _context.Employee.Where(a => a.SystemUserId == v.Id).FirstOrDefault() != null ?
                               "/" + _context.Employee.Where(a => a.SystemUserId == v.Id).FirstOrDefault().ProfilePicture :
                                    "/assets/images/user/avatar-2.jpg"
                       }).OrderBy(m => m.Name).ToList();
            var onlineUserIds = friendOnlineDetails.Select(m => m.UserID).ToArray();
            obj = obj.Where(m => onlineUserIds.Contains(m.UserID)).ToList();
            //obj.ForEach(m =>
            //    {
            //        m.ConnectionID = friendOnlineDetails.Where(x => x.UserID == m.UserID).Select(x => x.ConnectionID).ToList();
            //    });
            return obj;
        }
        public Employee GetUserById(string userId)
        {
            //var obj = _context.ApplicationUser.Where(m => m.Id == userId).FirstOrDefault();
            var obj = _context.Employee.Where(m => m.SystemUserId == userId).FirstOrDefault();
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
                             ProfilePicture = userImage(m.Id),
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
                ProfilePicture = userImage(m.Id),
                //Gender = m.Gender,
                IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
            }).ToList();
            return users;
        }


        public MessageRecords GetChatMessagesByUserID(string currentUserID, string toUserID, int lastMessageID = 0)
        {
            MessageRecords obj = new MessageRecords();
            var messages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserID == toUserID || m.FromUserID == toUserID) && (m.ToUserID == currentUserID || m.FromUserID == currentUserID)).OrderByDescending(m => m.CreatedOn);
            if (lastMessageID > 0)
            {
                obj.Messages = messages.Where(m => m.ChatMessageID < lastMessageID).Take(20).ToList().OrderBy(m => m.CreatedOn).ToList();
            }
            else
            {
                obj.Messages = messages.Take(20).ToList().OrderBy(m => m.CreatedOn).ToList();
            }
            obj.LastChatMessageId = obj.Messages.OrderBy(m => m.ChatMessageID).Select(m => m.ChatMessageID).FirstOrDefault();
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

        public UserModel GetUserModel(string id, Employee objentity = null, string friendRequestStatus = "", bool isRequestReceived = false)
        {
            var user = new Employee();
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
                objmodel.UserID = user.SystemUserId;
                objmodel.Name = user.FirstName;
                objmodel.ProfilePicture = userImage(user.SystemUserId);// user.ProfilePicture; // CommonFunctions.GetProfilePicture(user.ProfilePicture, user.Gender);
                objmodel.Gender = user.Gender;
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

        public string IdentityId()
        {
            var a = User.Identities.ToArray();
            var b = a[0].Claims.ToArray();
            var userID = b[0].Value;//  Context.User.Identity.Name;
            return userID;
        }


        public string userImage(string userId)
        {
            string ProfilePicture = "/assets/images/user/avatar-2.jpg";
            var existing = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault();
            ProfilePicture = existing != null ?
                               "/" + _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().ProfilePicture :
                                    "/assets/images/user/avatar-2.jpg";
            return ProfilePicture;
        }
        public string userFirstName(string userId)
        {
            string FirstName = "عميل";
            var existing = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault();
            FirstName = existing != null ?
                                    _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().FirstName :
                                    _context.Contractors.Where(a => a.ContractorUserId == userId).FirstOrDefault().ContractorName;
            return FirstName;
        }
        #endregion

        public async Task<IActionResult> UserNotifications()
        {
            var currenntUser = await _userManager.GetUserAsync(User);

            var list1 = _context.UserNotifications.Where(x => x.ToUserID == currenntUser.Id)
                  .OrderByDescending(x => x.CreatedOn).ToList();

            list1.ForEach(a => a.Status = "Read");
            _context.SaveChanges();

            List<UserNotificationDTO> userNotificationDTOs = new List<UserNotificationDTO>();


            foreach (UserNotification userNotification in list1)
            {
                UserNotificationDTO userNotificationDTO = new UserNotificationDTO
                {
                    FromUser = userFirstName(userNotification.FromUserID),
                    FromUserImage = userImage(userNotification.FromUserID),
                    NotificationType = userNotification.NotificationType,
                    CreatedOn = userNotification.CreatedOn,
                };
                userNotificationDTOs.Add(userNotificationDTO);
            }

            return View(userNotificationDTOs);
        }
    }
}
