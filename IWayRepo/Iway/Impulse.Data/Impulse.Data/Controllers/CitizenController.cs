using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impulse.Data.BLL.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Impulse.Model;
namespace Impulse.Data.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizen _citizen;

        const string key = "E546C8DF278CD5931069B522E695D4F2";
        public CitizenController(ICitizen __citizen)
        {
            this._citizen = __citizen;

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Citizen value)
        {
            return Ok(await _citizen.SaveCitizenDetails(value));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _citizen.getCitizenDetails(id));
        }
    }
}