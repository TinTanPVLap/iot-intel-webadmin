//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace adminiotintel.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking_Event
    {
        public decimal EventID { get; set; }
        public int RoomID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime TimeStart { get; set; }
        public System.DateTime TimeEnd { get; set; }
        public int UserHostID { get; set; }
        public int UserCreatedID { get; set; }
        public int UserUpdatedID { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
    }
}
