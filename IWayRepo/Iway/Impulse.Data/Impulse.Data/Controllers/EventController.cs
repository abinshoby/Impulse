using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impulse.Data.BLL.Interface;
using Impulse.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Impulse.Data.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEvent _event;

        const string key = "E546C8DF278CD5931069B522E695D4F2";
        public EventController(IEvent __event)
        {
            this._event = __event;

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EventViewModelPost value)
        {
            return Ok(await _event.SaveEventDetails(value));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]EventViewModelPost value)
        {
            return Ok(await _event.UpdateEventDetails(value));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _event.Delete(id));
        }
        [Route("AssingnVendorsToEvent")]
        [HttpPost]
        public async Task<IActionResult> AssingnVendorsToEvent([FromBody] List<VendorInvitation> value)
        {
            return Ok(await _event.AssingnVendorsToEvent(value));
        }
        [HttpPut]
        [Route("UpdateEventStatus")]
        public async Task<IActionResult> UpdateEventStatus([FromBody] AssignVendorEventPUT value)
        {
            return Ok(await _event.UpdateEventStatus(value));
        }
        [HttpPut]
        [Route("UpdateVendorsInvitationStatus")]
        public async Task<IActionResult> UpdateVendorsInvitationStatus([FromBody] VendorInvitation value)
        {
            return Ok(await _event.UpdateVendorsInvitationStatus(value));
        }

        [HttpGet]
        [Route("GetAdminEventDetails/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            return Ok(await _event.GetEvent(id));

        }

        [HttpGet]
        [Route("GetAllCorporateEvent/{id}")]
        public async Task<IActionResult> GetAllCorporateEvent(int id)
        {
            return Ok(await _event.GetAllEventByCorporateId(id));

        }

        [HttpGet]
        [Route("GetCorporateEvent/{id}")]
        public async Task<IActionResult> GetCorporateEvent(int id)
        {
            return Ok(await _event.GetCorporateEvent(id));

        }
        [HttpGet]
        [Route("GetAllByVendorId/{id}")]
        public async Task<IActionResult> GetAllByVendorId(int id)
        {
            return Ok(await _event.GetAllByVendorId(id));

        }
        [HttpGet]
        [Route("GetVendorEvent/{id}/{vID}")]
        public async Task<IActionResult> GetVendorEvent(int id, int vID)
        {
            return Ok(await _event.GetVendorEvent(id, vID));

        }

    }
}