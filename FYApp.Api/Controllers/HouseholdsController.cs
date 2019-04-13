using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FYApp.Api.Services;
using FYApp.Core.Models;

namespace FYApp.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HouseholdsController : ControllerBase
    {
        private readonly IFYCoreService _svc;

        public HouseholdsController()
        {
            _svc = new FYCoreService();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IList<Household>> Get()
        {
            var Households = _svc.GetAllHouseholds();
            return Households;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Household> Get(int id)
        {
            return _svc.GetHouseholdById(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Household> Post([FromBody] Household value)
        {            
            if (ModelState.IsValid)
            {
                return Ok(_svc.AddHousehold(value));              
            }
            return BadRequest() ;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Household value)
        {
            var household = _svc.GetHouseholdById(id);
            if (household == null) {
                return NotFound();
            }
         
            if ( _svc.UpdateHousehold(value))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }        
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
             var household = _svc.GetHouseholdById(id);
            if (household == null) {
                return NotFound();
            }

            _svc.DeleteHousehold(id);
            return Ok();
        }

    }
}
