
using Iway.Interface;
using Iway.Repository.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Repository
{

    public class TokenRepo : IToken
    {
        const string baseUri = "http://107.23.201.162:8081/api/token";
        public async Task<string> GetToken()
        {
            LoginModel login = new LoginModel();
            login.Username = "mario";
            login.Password = "secret";
            var requestUri = $"{baseUri}";
            //var response = await HttpRequestFactory.Post(requestUri, login);
            var response = await HttpRequestFactory.Post(requestUri, login);
            Console.WriteLine($"Status: {response.StatusCode}");
            //Console.WriteLine(response.ContentAsString());
            string outputModel = response.ContentAsString();
            outputModel = outputModel.Substring(1, outputModel.Length - 2);
            return await Task.Run(() => outputModel);
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class Jwt
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
        }
        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
        }
    }
}
