
using Impulse.Model;
using Iway.Interface;
using Iway.Repository.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Repository
{
    public class LoginRepo : ILogin
    {
        const string baseUri = "http://107.23.201.162:8081/api/Values/";

      
        public async Task<bool> Mail(string token, User _input)
        {

            User result = new User();
            var requestUri = "http://107.23.201.162:8080/SendCorporateSignupMail?toMailId=" + _input.UserName + "&&ToUserFullName=" + _input.Team.TeamName;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            return await Task.Run(() => true);

        }
        public async Task<bool> VendorMail(string token, User _input)
        {

            User result = new User();
            var requestUri = "http://107.23.201.162:8080/SendVendorSignupMail?toMailId=" + _input.UserName + "&&ToUserFullName=" + _input.Team.TeamName;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            return await Task.Run(() => true);

        }
        public async Task<User> Login(string token, Login _input)
        {
            User result = new User();
            var requestUri = $"{baseUri}"+ "/logincheck";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<User>();
            return await Task.Run(() => result);
        }
        public async Task<User> GetUserDetails(string token, int id)
        {           
            User result = new User();
            var requestUri = $"{baseUri}" + Convert.ToInt32(id) + "";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<User>();
            return await Task.Run(() => result);
        }
        public async Task<User> AddUserDetails(string token, User _input)
        {
            User result = new User();
            var requestUri = $"{baseUri}";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<User>();
            return await Task.Run(() => result);
        }
    }
}
