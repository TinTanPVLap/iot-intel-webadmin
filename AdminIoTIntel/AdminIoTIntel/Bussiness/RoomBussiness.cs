using adminiotintel.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminiotintel.Bussiness
{
    public class RoomBussiness
    {
        IOT_Intel_SS10Entities db = new IOT_Intel_SS10Entities();

        public List<Rooms> getListRoomBySearch(string keySearch)
        {
            List<Rooms> lstRooms = db.Rooms.Where(x => (keySearch == "" || x.Name.Contains(keySearch))).ToList();
            return lstRooms;
        }

        public Rooms getRoomByID(int roomID)
        {
            return db.Rooms.Where(x => x.RoomID == roomID).FirstOrDefault();
        }

        public bool CheckInsert(string roomName)
        {
            Rooms room = db.Rooms.Where(x => x.Name == roomName).FirstOrDefault();
            if (room == null)
                return true;
            else
                return false;
        }

        public bool InsertRoom(Rooms room)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    entityObject.Rooms.Add(room);
                    entityObject.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Checkupdate(int roomID ,string roomName)
        {
            Rooms room = db.Rooms.Where(x => x.Name == roomName && x.RoomID != roomID).FirstOrDefault();
            if (room == null)
                return true;
            else
                return false;
        }

        public bool UpdatetRoom(int roomID, string roomName, string description, int userUpdate)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Rooms rooms = entityObject.Rooms.Where(x => x.RoomID == roomID).FirstOrDefault();
                    if (rooms != null)
                    {
                        rooms.Name = roomName;
                        rooms.Description = description;
                        rooms.UserUpdatedID = userUpdate;
                        rooms.DateUpdated = DateTime.Now;
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

        public bool CheckDelete(int roomID)
        {
            List<Booking_Event> lstEvent = db.Booking_Event.Where(x => x.RoomID == roomID).ToList();
            if (lstEvent.Count > 0)
                return false;
            else
                return true;
        }

        public bool DeleteRoom(int roomID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Rooms item = entityObject.Rooms.Where(s => s.RoomID == roomID).FirstOrDefault();
                    if (item != null)
                    {
                        entityObject.Rooms.Remove(item);
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