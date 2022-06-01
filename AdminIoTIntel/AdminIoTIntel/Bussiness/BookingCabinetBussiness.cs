using adminiotintel.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace adminiotintel.Bussiness
{
    public class BookingCabinetBussiness
    {
        IOT_Intel_SS10Entities db = new IOT_Intel_SS10Entities();

        public List<GetListBookingCabinet_Result> getListBookingCabinet(string keyWord, int cabinetID)
        {
            List<GetListBookingCabinet_Result> lstResult = db.GetListBookingCabinet().Where(x => (x.CabinetName.Contains(keyWord) || x.FullName.Contains(keyWord)
            || keyWord == "") && (x.CabinetID == cabinetID || cabinetID == 1000)).ToList();
            return lstResult;
        }

        public Booking_Cabinet getBookingByID(decimal bookingID)
        {
            return db.Booking_Cabinet.Where(x => x.BookingID == bookingID).FirstOrDefault();
        }

        public Booking_Cabinet getBookingByEventID(decimal eventID)
        {
            return db.Booking_Cabinet.Where(x => x.EventID == eventID).FirstOrDefault();
        }

        public bool CheckInsert(int cabinetID, DateTime timeStart, DateTime timeEnd)
        {
            List<Booking_Cabinet> lstBooking = db.Booking_Cabinet.Where(x => x.CabinetID == cabinetID &&
             (timeStart > x.TimeStart && timeStart < x.TimeEnd ||
             timeEnd > x.TimeStart && timeEnd < x.TimeEnd)).ToList();
            if (lstBooking.Count > 0)
            {
                TimeSpan start = new TimeSpan(timeStart.Hour, timeStart.Minute, 0);
                TimeSpan end = new TimeSpan(timeEnd.Hour, timeEnd.Minute, 0);
                foreach (Booking_Cabinet item in lstBooking)
                {
                    TimeSpan startBooking = new TimeSpan(item.TimeStart.Hour, item.TimeStart.Minute, 0);
                    TimeSpan endtBooking = new TimeSpan(item.TimeEnd.Hour, item.TimeEnd.Minute, 0);
                    if (start >= startBooking && start <= endtBooking || end >= startBooking && end <= endtBooking)
                        return false;
                }
            }

            return true;
        }

        public bool InsertBooking(Booking_Cabinet booking)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    entityObject.Booking_Cabinet.Add(booking);
                    entityObject.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Checkupdate(decimal bookingID, int cabinetID, DateTime timeStart, DateTime timeEnd)
        {
            List<Booking_Cabinet> lstBooking = db.Booking_Cabinet.Where(x => x.CabinetID == cabinetID &&
             (timeStart > x.TimeStart && timeStart < x.TimeEnd ||
             timeEnd > x.TimeStart && timeEnd < x.TimeEnd) && x.BookingID != bookingID).ToList();
            if (lstBooking.Count > 0)
            {
                TimeSpan start = new TimeSpan(timeStart.Hour, timeStart.Minute, 0);
                TimeSpan end = new TimeSpan(timeEnd.Hour, timeEnd.Minute, 0);
                foreach (Booking_Cabinet item in lstBooking)
                {
                    TimeSpan startBooking = new TimeSpan(item.TimeStart.Hour, item.TimeStart.Minute, 0);
                    TimeSpan endtBooking = new TimeSpan(item.TimeEnd.Hour, item.TimeEnd.Minute, 0);
                    if (start >= startBooking && start <= endtBooking || end >= startBooking && end <= endtBooking)
                        return false;
                }
            }

            return true;
        }

        public bool UpdatetBooking(decimal bookingID, int cabinetID, int pinCode, int userHost, DateTime timeStart, DateTime timeEnd, int userUpdate)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Booking_Cabinet booking = entityObject.Booking_Cabinet.Where(x => x.BookingID == bookingID).FirstOrDefault();
                    if (booking != null)
                    {
                        booking.CabinetID = cabinetID;
                        booking.Pincode = pinCode;
                        booking.UserHostID = userHost;
                        booking.TimeStart = timeStart;
                        booking.TimeEnd = timeEnd;
                        booking.UserUpdatedID = userUpdate;
                        booking.DateUpdated = DateTime.Now;
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

        public bool CheckDelete(decimal bookingID)
        {
            Booking_Cabinet booking = db.Booking_Cabinet.Where(x => x.BookingID == bookingID).FirstOrDefault();
            if (booking != null)
            {
                if (booking.TimeStart < DateTime.Now)
                    return false;
            }
            return true;
        }

        public bool DeleteBooking(decimal bookingID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Booking_Cabinet item = entityObject.Booking_Cabinet.Where(s => s.BookingID == bookingID).FirstOrDefault();
                    if (item != null)
                    {
                        entityObject.Booking_Cabinet.Remove(item);
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