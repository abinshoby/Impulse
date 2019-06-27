using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Interface
{
    public interface IUser
    {
        Task<List<User>> GetPendingList(string token);
        Task<List<User>> GetUserListByType(string token, int typeId);
        Task<User> UpadtePendingListStatus(string token, User _input);
        Task<List<VendorInterest>> GetVendorInterestByUser(string token, int UserId);
        Task<bool> PostVendorInterest(string token, List<VendorInterest> _input);
        Task<VendorInterest> GetVendorInterestDetails(string token, int VID);
        Task<bool> PutVendorInterest(string token, List<VendorInterest> _input);

        Task<Citizen> GetCitizenDetails(string token, int UID);
        Task<bool> PostCitizenDetails(string token, Citizen _input);
    }
}
