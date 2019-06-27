using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impulse.Data.BLL.Handler;
using Impulse.Data.BLL.Interface;
using Impulse.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Impulse.Data.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IUser _user;

        const string key = "E546C8DF278CD5931069B522E695D4F2";
        public ValuesController(IUser user)
        {
            this._user = user;

        }
        [HttpPost]
        [Route("LoginCheck")]
        public async Task<IActionResult> LoginCheck([FromBody]Login value)
        {
            return Ok(await _user.LoginCheck(value));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User value)
        {
            return Ok(await _user.Post(value));
        }
        [HttpPost]
        [Route("PostCompanyDetails")]
        public async Task<IActionResult> PostCompanyDetails([FromBody]User value)
        {
            return Ok(await _user.PostCompanyDetails(value));
        }
        [Route("PutPendingUser")]
        [HttpPost]
        public async Task<IActionResult> PutPendingUser([FromBody]User value)
        {
            return Ok(await _user.PutPendingUser(value));
        }
        [Route("PostVendorInterest")]
        [HttpPost]
        public async Task<IActionResult> PostVendorInterest([FromBody]List<VendorInterest> value)
        {
            return Ok(await _user.PostVendorInterest(value));
        }





        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _user.Get());

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _user.Get(Id));

        }
        [HttpGet]
        [Route("GetPendingList")]
        public async Task<IActionResult> GetPendingList()
        {
            return Ok(await _user.GetPendingList());

        }
        [HttpGet]
        [Route("GetUserListByType/{id}")]
        public async Task<IActionResult> GetUserListByType(int id)
        {
            return Ok(await _user.GetUserListByType(id));

        }
        [HttpGet]
        [Route("GetVendorInterestDetails/{id}")]
        public async Task<IActionResult> GetVendorInterestDetails(int id)
        {
            return Ok(await _user.GetVendorInterestDetails(id));

        }

        // PUT: api/Default/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]User value)
        {
            return Ok(await _user.Put(value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _user.Delete(id));
        }

        [HttpGet]
        [Route("GetVendorInterestbyUser/{id}")]
        public async Task<IActionResult> GetVendorInterestbyUser(int id)
        {
            return Ok(await _user.GetVendorInterestbyUser(id));

        }

        [HttpGet]
        [Route("GetVendorInterest/{eventTId}/{eventSTId}/{EvenDetailsID}")]
        public async Task<IActionResult> GetVendorInterest(int eventTId, int eventSTId, int EvenDetailsID)
        {
            return Ok(await _user.GetVendorInterest(eventTId, eventSTId, EvenDetailsID));

        }

        [Route("UpdateVendorInterest")]
        [HttpPut]
        public async Task<IActionResult> UpdateVendorInterest([FromBody]List<VendorInterest> value)
        {
            return Ok(await _user.UpdateVendorInterest(value));
        }


    }
}
