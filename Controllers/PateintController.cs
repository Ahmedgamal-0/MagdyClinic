using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Controllers
{
    [Route("Magdy")]
    public class PateintController:Controller
    {
        [HttpGet("")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
