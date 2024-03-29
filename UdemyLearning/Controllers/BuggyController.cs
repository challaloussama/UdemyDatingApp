﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyLearning.Data;
using UdemyLearning.Entities;

namespace UdemyLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly DataContext context;

        public BuggyController(DataContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

    
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = context.Users.Find(-1);
            if (thing == null) return NotFound();

            return Ok(thing);
        }

  
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = context.Users.Find(-1);
            var thingToReturn = thing.ToString();
            return thingToReturn;
        }

   
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
