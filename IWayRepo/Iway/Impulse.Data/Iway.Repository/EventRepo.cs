using Impulse.Model;
using Iway.Interface;
using Iway.Repository.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Repository
{
    public class EventRepo : IEvent
    {
        const string baseUri = "http://107.23.201.162:8081/api/Event/";
        const string HostedUri = "http://107.23.201.162:8081/api/Event/";
        const string HostedUriUser = "http://107.23.201.162:8081/api/values/";
        public async Task<List<EventViewModel>> GetCorporateEventList(string token, int id)
        {
            HttpClient _httpClient = new HttpClient(); ;
            List<EventViewModel> result = new List<EventViewModel>();
            var requestUri = $"http://107.23.201.162:8080/GetAllEventByCorporate/" + Convert.ToInt32(id) + "";
            var response = await _httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            result = response.ContentAsType<List<EventViewModel>>();
            return await Task.Run(() => result);

        }
        public async Task<List<EventViewModel>> GetAdminEventList(string token)
        {
            HttpClient _httpClient = new HttpClient(); ;
            List<EventViewModel> result = new List<EventViewModel>();
            var requestUri = $"http://107.23.201.162:8080/GetAllEvents/";
            var response = await _httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            result = response.ContentAsType<List<EventViewModel>>();
            return await Task.Run(() => result);

        }
        public async Task<List<EventViewModel>> GetVendorEventList(string token, int id)
        {
            List<EventViewModel> result = new List<EventViewModel>();
            var requestUri = $"{baseUri}" + "/GetAllByVendorId/" + Convert.ToInt32(id) + "";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<EventViewModel>>();
            return await Task.Run(() => result);

        }
        public async Task<EventViewModel> GetEventDetailsWithEventId(string token, int id)
        {
            EventViewModel result = new EventViewModel();
            var requestUri = $"{baseUri}" + "/GetAdminEventDetails/" + Convert.ToInt32(id) + "";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<EventViewModel>();
            return await Task.Run(() => result);

        }
        public async Task<EventViewModel> GetCoEventDetailsWithEventId(string token, int id)
        {
            EventViewModel result = new EventViewModel();
            var requestUri = $"{HostedUri}" + "/GetCorporateEvent/" + Convert.ToInt32(id) + "";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<EventViewModel>();
            return await Task.Run(() => result);

        }
        public async Task<EventViewModel> GetVendorEventDetailsWithEventId(string token, int id, int VId)
        {
            EventViewModel result = new EventViewModel();
            var requestUri = $"{HostedUri}" + "/GetVendorEvent/" + Convert.ToInt32(id) + "/" + Convert.ToInt32(VId);
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<EventViewModel>();
            return await Task.Run(() => result);

        }
        public async Task<bool> AddEventDetails(string token, EventViewModelPost _input)
        {
            bool result;
            var requestUri = $"{baseUri}";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
        public async Task<bool> UpdaeEventDetails(string token, EventViewModelPost _input)
        {
            bool result;
            var requestUri = $"{HostedUri}";
            var response = await HttpRequestFactory.Put(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
        public async Task<bool> AssingnVendorsToEvent(string token, List<VendorInvitation> _input)
        {
            bool result;
            var requestUri = $"{HostedUri}" + "/AssingnVendorsToEvent/";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
        public async Task<bool> UpdateVendorsInvitationStatus(string token, VendorInvitation _input)
        {
            bool result;
            var requestUri = $"{HostedUri}" + "/UpdateVendorsInvitationStatus/";
            var response = await HttpRequestFactory.Put(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
        public async Task<List<User>> GetVendorInterestUser(string token, int eventTId, int eventSTId, int EvenDetailsID)
        {
            List<User> result = new List<User>();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8081/api/values/GetVendorInterest/" + eventTId + "/" + eventSTId + "/" + EvenDetailsID;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<User>>();
            return await Task.Run(() => result);

        }
        public async Task<List<EventType>> GetEventType(string token)
        {
            List<EventType> result = new List<EventType>();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8080/EventType/";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<EventType>>();
            return await Task.Run(() => result);

        }
        public async Task<List<EventSubTypeModel>> GetEventSubType(string token, int EventId)
        {
            List<EventSubTypeModel> result = new List<EventSubTypeModel>();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8080/EventSubTypeByEventTypeId/" + EventId;
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<EventSubTypeModel>>();
            return await Task.Run(() => result);

        }
        public async Task<List<ScheduleType>> GetScheduleType(string token)
        {
            List<ScheduleType> result = new List<ScheduleType>();
            //var requestUri = $"{baseUri}" + "/GetUserListByType/" + typeId;
            var requestUri = $"http://107.23.201.162:8080/ScheduleType/";
            var response = await HttpRequestFactory.Get(requestUri, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<List<ScheduleType>>();
            return await Task.Run(() => result);

        }
        public async Task<bool> PostVendorInterest(string token, List<VendorInterest> _input)
        {
            bool result;
            var requestUri = $"http://107.23.201.162:8081/api/Values/" + "PostVendorInterest/";
            var response = await HttpRequestFactory.Post(requestUri, _input, token);
            Console.WriteLine($"Status: {response.StatusCode}");
            result = response.ContentAsType<bool>();
            return await Task.Run(() => result);
        }
    }
}
