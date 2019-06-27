using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Interface
{
    public interface IEvent
    {
        Task<List<EventViewModel>> GetCorporateEventList(string token, int id);
        Task<List<EventViewModel>> GetAdminEventList(string token);
        Task<List<EventViewModel>> GetVendorEventList(string token, int id);
        Task<EventViewModel> GetEventDetailsWithEventId(string token, int id);
        Task<bool> AddEventDetails(string token, EventViewModelPost _input);
        Task<EventViewModel> GetCoEventDetailsWithEventId(string token, int id);
        Task<EventViewModel> GetVendorEventDetailsWithEventId(string token, int id, int VId);
        Task<bool> UpdaeEventDetails(string token, EventViewModelPost _input);
        Task<bool> UpdateVendorsInvitationStatus(string token, VendorInvitation _input);
        Task<List<User>> GetVendorInterestUser(string token, int eventTId, int eventSTId, int EvenDetailsID);
        Task<List<EventSubTypeModel>> GetEventSubType(string token, int EventId);
        Task<List<EventType>> GetEventType(string token);
        Task<List<ScheduleType>> GetScheduleType(string token);
        Task<bool> AssingnVendorsToEvent(string token, List<VendorInvitation> _input);
        Task<bool> PostVendorInterest(string token, List<VendorInterest> _input);
    }
}
