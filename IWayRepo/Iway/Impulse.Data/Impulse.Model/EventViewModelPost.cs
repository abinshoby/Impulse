using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Impulse.Model
{

    public class EventViewModelPost
    {
        public int EventID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime ExpectedToConduct { get; set; }
        public string EmployeeRange { get; set; }
        public Nullable<long> CorporateId { get; set; }       
        public string Location { get; set; }
        public List<EventDetailsViewModelPost> EventDetails { get; set; }
        public int EventTypeId { get; set; }
        public int EventSubTypeId { get; set; }
        public List<SelectListItem> ddlEventType { get; set; }
        public List<SelectListItem> ddlEventSubType { get; set; }
        public List<ScheduleType> ScheduleTypeList { get; set; }
        public string ScheduleType { set; get; }
        public int? ScheduleTypeId { set; get; }
        public int[] EventSubTypeList { get; set; }

    }
    public class EventDetailsViewModelPost
    {
        public int EventDetailsID { get; set; }
        public Nullable<int> EventTypeId { get; set; }
        public Nullable<int> EventSubTypeId { get; set; }
        public int SurfaceTypeId { get; set; }
        public string EmployeeRange { get; set; }
        public DateTime ExpectedToConduct { get; set; }
        public Nullable<int> ScheduleTypeId { get; set; }
        public string Location { get; set; }

    }
    public class AssignVendorEventPUT
    {
        public long EventId { get; set; }
        public long VendorId { get; set; }
        public int? StatusId { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class EventViewModelVendorPUT
    {
        public long EventId { get; set; }
        public int StatusId { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class EventPlaceViewModelPOST
    {
        public int? EventSubTypeId { get; set; }
        public int? EventTypeId { get; set; }
        public int PlaceId { get; set; }
    }
    public class EventPlaceViewModelPUT
    {
        public int EventypeLocationId { get; set; }
        public int? EventSubTypeId { get; set; }
        public int? EventTypeId { get; set; }
        public int PlaceId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
