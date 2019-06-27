using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Impulse.Data.BLL.Interface
{
    public interface IEvent
    {
        Task<bool> SaveEventDetails(EventViewModelPost modal);
        Task<bool> UpdateEventDetails(EventViewModelPost modal);
        Task<bool> Delete(int id);
        Task<bool> UpdateEventStatus(AssignVendorEventPUT modal);
        Task<bool> AssingnVendorsToEvent(List<VendorInvitation> modal);
        Task<bool> UpdateVendorsInvitationStatus(VendorInvitation modal);
        Task<EventViewModel> GetEvent(int id);
        Task<User> GetUser(int Id);
        string GetUserNameById(int userId);
        Task<EventViewModel> GetCorporateEvent(int id);
        Task<List<EventViewModel>> GetAllByVendorId(int Id);
        Task<EventViewModel> GetVendorEvent(int Eventid, int vID);
        Task<List<EventViewModel>> GetAllEventByCorporateId(int Id);
    }
}
