using adminiotintel.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace adminiotintel.Bussiness
{
    public class BookingEventBussiness
    {
        IOT_Intel_SS10Entities db = new IOT_Intel_SS10Entities();
        public List<GetListBookingEvent_Result> getListBookingEvent(string keyWord, int roomID)
        {
            List<GetListBookingEvent_Result> lstResult = db.GetListBookingEvent().Where(x => (x.Title.Contains(keyWord) || x.FullName.Contains(keyWord)
            || keyWord == "") && (x.RoomID == roomID || roomID == 1000)).ToList();
            return lstResult;
        }

        public Booking_Event getBookingEventByID(decimal eventID)
        {
            return db.Booking_Event.Where(x => x.EventID == eventID).FirstOrDefault();
        }

        public bool CheckInsert(int roomID, DateTime timeStart, DateTime timeEnd)
        {
            List<Booking_Event> lstBooking = db.Booking_Event.Where(x => x.RoomID == roomID &&
                (timeStart > x.TimeStart && timeStart < x.TimeEnd ||
                timeEnd > x.TimeStart && timeEnd < x.TimeEnd)).ToList();
            if (lstBooking.Count > 0)
            {
                TimeSpan start = new TimeSpan(timeStart.Hour, timeStart.Minute, 0);
                TimeSpan end = new TimeSpan(timeEnd.Hour, timeEnd.Minute, 0);
                foreach (Booking_Event item in lstBooking)
                {
                    TimeSpan startBooking = new TimeSpan(item.TimeStart.Hour, item.TimeStart.Minute, 0);
                    TimeSpan endtBooking = new TimeSpan(item.TimeEnd.Hour, item.TimeEnd.Minute, 0);
                    if (start >= startBooking && start <= endtBooking || end >= startBooking && end <= endtBooking)
                        return false;
                }
            }

            return true;
        }

        public bool InsertBooking(Booking_Event booking, List<BookingUsers> lstBkUser)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    entityObject.Booking_Event.Add(booking);
                    entityObject.SaveChanges();
                    InsertBookingUser(lstBkUser, booking.EventID);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckUpdate(decimal eventID, int roomID, DateTime timeStart, DateTime timeEnd)
        {
            List<Booking_Event> lstBooking = db.Booking_Event.Where(x => x.RoomID == roomID &&
                (timeStart > x.TimeStart && timeStart < x.TimeEnd ||
                timeEnd > x.TimeStart && timeEnd < x.TimeEnd) && x.EventID != eventID).ToList();
            if (lstBooking.Count > 0)
            {
                TimeSpan start = new TimeSpan(timeStart.Hour, timeStart.Minute, 0);
                TimeSpan end = new TimeSpan(timeEnd.Hour, timeEnd.Minute, 0);
                foreach (Booking_Event item in lstBooking)
                {
                    TimeSpan startBooking = new TimeSpan(item.TimeStart.Hour, item.TimeStart.Minute, 0);
                    TimeSpan endtBooking = new TimeSpan(item.TimeEnd.Hour, item.TimeEnd.Minute, 0);
                    if (start >= startBooking && start <= endtBooking || end >= startBooking && end <= endtBooking)
                        return false;
                }
            }

            return true;
        }
        public bool UpdatetBooking(decimal eventID, int roomID, string title, int userHost, DateTime timeStart, DateTime timeEnd, string description, int userUpdate,
            List<BookingUsers> lstBkUser)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Booking_Event booking = entityObject.Booking_Event.Where(x => x.EventID == eventID).FirstOrDefault();
                    if (booking != null)
                    {
                        booking.RoomID = roomID;
                        booking.Title = title;
                        booking.UserHostID = userHost;
                        booking.TimeStart = timeStart;
                        booking.TimeEnd = timeEnd;
                        booking.Description = description;
                        booking.UserUpdatedID = userUpdate;
                        booking.DateUpdated = DateTime.Now;
                    }
                    entityObject.SaveChanges();
                    InsertBookingUser(lstBkUser, eventID);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckDelete(decimal eventID)
        {
            Booking_Event booking = db.Booking_Event.Where(x => x.EventID == eventID).FirstOrDefault();
            if (booking != null)
            {
                if (booking.TimeStart < DateTime.Now)
                    return false;
            }
            return true;
        }

        public bool DeleteBooking(decimal eventID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Booking_Event item = entityObject.Booking_Event.Where(s => s.EventID == eventID).FirstOrDefault();
                    if (item != null)
                    {
                        entityObject.Booking_Event.Remove(item);
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

        public List<getListBookingUser_Result>  getListBookingUser(decimal eventID)
        {
            return db.getListBookingUser(eventID).OrderBy(x => x.FullName).ToList();
        }

        public bool InsertBookingUser(List<BookingUsers> lstBkUser, decimal eventID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    List<BookingUsers> lstData = entityObject.BookingUsers.Where(x => x.BookingID == eventID).ToList();
                    if (lstData.Count > 0)
                    {
                        foreach (BookingUsers item in lstData)
                        {
                            entityObject.BookingUsers.Remove(item);
                        }    
                    }
                    foreach (BookingUsers item in lstBkUser)
                    {
                        BookingUsers bkUser = new BookingUsers();
                        bkUser.BookingID = eventID;
                        bkUser.UserID = item.UserID;
                        entityObject.BookingUsers.Add(bkUser);
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

        public bool DeleteBookingUser(decimal eventID, int userID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    BookingUsers item = entityObject.BookingUsers.Where(s => s.BookingID == eventID && s.UserID == userID).FirstOrDefault();
                    if (item != null)
                    {
                        entityObject.BookingUsers.Remove(item);
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