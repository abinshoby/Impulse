using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{
    public class VendorInterest
    {
        public long VendorInterestId { get; set; }
        public long? UserId { get; set; }
        public long? PlaceId { get; set; }
        public long? YearOfExperience { get; set; }
        public string Place { get; set; }
        public long? EventTypeId { get; set; }
        public long? EventSubTypeId { get; set; }
        public string RecordStatus { get; set; }
        public User User { get; set; }
        public List<VendorInterestRate> VendorInterestRate { get; set; }
        public EventType EventType { get; set; }
        public EventSubType EventSubType { get; set; }
        public List<SelectListItem> ddlEventType { get; set; }
        public List<SelectListItem> ddlEventSubType { get; set; }

        public List<VendorInterest> VendorInterestProfieVlaue { get; set; }

        public int[] EventSubTypeList { get; set; }

    }

}
