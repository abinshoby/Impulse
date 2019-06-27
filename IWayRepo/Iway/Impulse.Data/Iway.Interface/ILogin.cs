using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Interface
{
    public interface ILogin
    {
        Task<User> AddUserDetails(string token, User _input);
        Task<bool> Mail(string token, User _input);
        Task<User> Login(string token, Login _input);
        Task<User> GetUserDetails(string token, int id);
        Task<bool> VendorMail(string token, User _input);
    }
}
