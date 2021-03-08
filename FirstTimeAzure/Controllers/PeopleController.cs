using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirstTimeAzure.Models;

namespace FirstTimeAzure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var people = new List<Person>
        {
            new Person
            {
                FirstName = "Artur",
                LastName = "Ziemianski",
                id = 1
            }
        };
            return Ok(people);
        }
    }

}
