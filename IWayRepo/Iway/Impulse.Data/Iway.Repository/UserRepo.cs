
using Impulse.Model;
using Iway.Interface;
using Iway.Repository.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Repository
{
    public class UserRepo : IUser
    {
        const string baseUri = "http://107.23.201.162:8081/api/Values";
        public async Task<List<User>> GetPendingList(string token)
        {
            List<User> result = new List<User>();
            var requestUri = $"{baseUri}" + "/GetPendingList";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<User>>();
            return await Task.Run(() => result);

        }
        public async Task<List<User>> GetUserListByType(string token, int typeId)
        {
            List<User> result = new List<User>();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8081/api/values/GetUserListByType/" + typeId;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<User>>();
            return await Task.Run(() => result);

        }

        public async Task<User> UpadtePendingListStatus(string token, User _input)
        {
            User result = new User();
            var requestUri = $"{baseUri}" + "/PutPendingUser/";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<User>();
            return await Task.Run(() => result);
        }

        public async Task<List<VendorInterest>> GetVendorInterestByUser(string token, int UserId)
        {
            List<VendorInterest> result = new List<VendorInterest>();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8081/api/values/GetVendorInterestbyUser/" + UserId;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<VendorInterest>>();
            return await Task.Run(() => result);

        }
        public async Task<bool> PostVendorInterest(string token, List<VendorInterest> _input)
        {
            bool result;
            var requestUri = $"{baseUri}" + "/PostVendorInterest/";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
        public async Task<bool> PutVendorInterest(string token, List<VendorInterest> _input)
        {
            bool result;
            var requestUri = $"{baseUri}" + "/UpdateVendorInterest/";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
        public async Task<VendorInterest> GetVendorInterestDetails(string token, int VID)
        {
            VendorInterest result = new VendorInterest();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8081/api/values/GetVendorInterestDetails/" + VID;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<VendorInterest>();
            return await Task.Run(() => result);

        }
        public async Task<Citizen> GetCitizenDetails(string token, int UID)
        {
            Citizen result = new Citizen();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8081/api/Citizen/" + UID;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<Citizen>();
            return await Task.Run(() => result);

        }
        public async Task<bool> PostCitizenDetails(string token, Citizen _input)
        {
            bool result;
            var requestUri = $"http://107.23.201.162:8081/api/Citizen/";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
    }
}
