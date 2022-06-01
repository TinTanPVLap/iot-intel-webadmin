using adminiotintel.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace adminiotintel.Bussiness
{
    public class CabinetBussiness
    {
        IOT_Intel_SS10Entities db = new IOT_Intel_SS10Entities();

        public List<Cabinet> getListCabinetBySearch(string keySearch)
        {
            List<Cabinet> lstCabinet = db.Cabinet.Where(x => (keySearch == "" || x.Name.Contains(keySearch))).ToList();
            return lstCabinet;
        }

        public Cabinet getCabinetByID(int cabinetID)
        {
            return db.Cabinet.Where(x => x.CabinetID == cabinetID).FirstOrDefault();
        }

        public bool CheckInsert(string cabinetName)
        {
            Cabinet cabinet = db.Cabinet.Where(x => x.Name == cabinetName).FirstOrDefault();
            if (cabinet == null)
                return true;
            else
                return false;
        }

        public bool InsertCabinet(Cabinet cabinet)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    entityObject.Cabinet.Add(cabinet);
                    entityObject.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Checkupdate(int cabinetID, string cabinetName)
        {
            Cabinet cabinet = db.Cabinet.Where(x => x.Name == cabinetName && x.CabinetID != cabinetID).FirstOrDefault();
            if (cabinet == null)
                return true;
            else
                return false;
        }

        public bool UpdatetRoom(int cabinetID, string cabinetName, byte status, int userUpdate)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Cabinet cabinet = entityObject.Cabinet.Where(x => x.CabinetID == cabinetID).FirstOrDefault();
                    if (cabinet != null)
                    {
                        cabinet.Name = cabinetName;
                        cabinet.Status = status;
                        cabinet.UserUpdatedID = userUpdate;
                        cabinet.DateUpdated = DateTime.Now;
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

        public bool CheckDelete(int cabinetID)
        {
            List<Booking_Cabinet> lstEvent = db.Booking_Cabinet.Where(x => x.CabinetID == cabinetID).ToList();
            if (lstEvent.Count > 0)
                return false;
            else
                return true;
        }

        public bool DeleteRoom(int cabinetID)
        {
            try
            {
                using (IOT_Intel_SS10Entities entityObject = new IOT_Intel_SS10Entities())
                {
                    Cabinet item = entityObject.Cabinet.Where(s => s.CabinetID == cabinetID).FirstOrDefault();
                    if (item != null)
                    {
                        entityObject.Cabinet.Remove(item);
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