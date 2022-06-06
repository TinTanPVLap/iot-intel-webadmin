using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using adminiotintel.model;

namespace adminiotintel.Bussiness
{
    public class UsersBussiness
    {
        IOT_Intel_SS10Entities db = new IOT_Intel_SS10Entities();

        public Users CheckingLogin(string username, string passWord)
        {
            return db.Users.Where(x => x.UserName == username && x.Password == passWord && x.Status == 1).FirstOrDefault();       
        }

        public Users GetUserByID(int userID)
        {
            return db.Users.Where(x => x.UserID == userID).FirstOrDefault();
        }

        public List<Users> getListAllUser()
        {
            return db.Users.ToList();
        }

        public List<Users> getListUserBySearch(string keySearch, int IsStatus)
        {
            List<Users> lstUser = db.Users.Where(x => (keySearch == "" || x.UserID.ToString() == keySearch || x.FullName.Contains(keySearch) || x.Email.Contains(keySearch))
             && (IsStatus == 100 || x.Status == IsStatus)).ToList();
            return lstUser;
        }

        public bool ResetPassword(int userID, string pass)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Users admin = entityObject.Users.Where(s => s.UserID == userID).FirstOrDefault();
                    if (admin != null)
                    {
                        admin.Password = pass;
                        admin.DateUpdated = DateTime.Now;
                        entityObject.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckInsert(string userName, string email)
        {
            Users users = db.Users.Where(x => x.UserName == userName || x.Email == email).FirstOrDefault();
            if (users == null)
                return true;
            else
                return false;
        }

        public bool InsertUser(Users users)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    entityObject.Users.Add(users);
                    entityObject.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckUpdate(int userID ,string userName, string email)
        {
            Users users = db.Users.Where(x => (x.UserName == userName || x.Email == email) && x.UserID != userID).FirstOrDefault();
            if (users == null)
                return true;
            else
                return false;
        }

        public bool UpdatetUser(int userID, int groupID ,string fullName, string userName, string email, string phone, byte active, string imagesPath, int userUpdate)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Users users = entityObject.Users.Where(x => x.UserID == userID).FirstOrDefault();
                    if (users != null)
                    {
                        users.GroupID = groupID;
                        users.FullName = fullName;
                        users.UserName = userName;
                        users.Email = email;
                        users.Phone = phone;
                        users.Status = active;
                        users.Images = imagesPath;
                        users.UserUpdatedID = userUpdate;
                        users.DateUpdated = DateTime.Now;
                    }    
                    entityObject.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckDelete(int userID)
        {
            List<Booking_Cabinet> lstBooking = db.Booking_Cabinet.Where(x => x.UserHostID == userID).ToList();
            List<Booking_Event> lstEvent = db.Booking_Event.Where(x => x.UserHostID == userID).ToList();
            if (lstBooking.Count > 0 || lstEvent.Count > 0)
                return false;
            else
                return true;
        }

        public bool DeleteUser(int userID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Users item = entityObject.Users.Where(s => s.UserID == userID).FirstOrDefault();
                    if (item != null)
                    {
                        if (System.IO.File.Exists(item.Images)) // xóa hình đã upload
                            System.IO.File.Delete(item.Images);

                        entityObject.Users.Remove(item);

                        List<Users_Fuctions> lstFunction = entityObject.Users_Fuctions.Where(x => x.UserID == userID).ToList();
                        if (lstFunction.Count > 0)
                        {
                            foreach (Users_Fuctions itemF in lstFunction)
                            {
                                entityObject.Users_Fuctions.Remove(itemF);
                            }    
                        }    
                        //Save to database
                        entityObject.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Users checkLogin(string _username, string _password)
        {
            Users user = db.Users.Where(s => s.UserName == _username && s.Password == _password && s.Status == 1).FirstOrDefault();
            if (user != null)
                return user;
            else
                return null;
        }

        public bool UpdatePassWord(int userID, string newPass, int userUpdate)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Users users = entityObject.Users.Where(x => x.UserID == userID).FirstOrDefault();
                    if (users != null)
                    {
                        users.Password = newPass;
                        users.UserUpdatedID = userUpdate;
                        users.DateUpdated = DateTime.Now;
                        entityObject.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}