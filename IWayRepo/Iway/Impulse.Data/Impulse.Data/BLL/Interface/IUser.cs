using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Impulse.Data.BLL.Interface
{
    public interface IUser
    {
        Task<User> LoginCheck(Login _input);
        Task<List<User>> Get();
        Task<User> Get(int Id);
        Task<List<User>> GetPendingList();
        Task<User> Post(User input);
        Task<User> Put(User input);
        Task<User> PostCompanyDetails(User user);
        Task<User> PutPendingUser(User user);
        Task<bool> Delete(int Id);
        Task<List<User>> GetUserListByType(int UserTypeId);
        Task<bool> PostVendorInterest(List<VendorInterest> input);
        Task<bool> UpdateVendorInterest(List<VendorInterest> input);
        Task<List<VendorInterest>> GetVendorInterestbyUser(int userId);
        Task<List<User>> GetVendorInterest(int eventTId, int eventSTId, int EvenDetailsID);
        Task<VendorInterest> GetVendorInterestDetails(int VIId);

    }
}
