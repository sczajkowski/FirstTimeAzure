using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherBackend.DbContext;

namespace WeatherBackend
{
    public class GetWeather
    {
        private readonly DatabaseContext _context;

        public GetWeather(DatabaseContext context)
        {
            _context = context;
        }

        [FunctionName("GetWeather")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req, ILogger log)
        {
            try
            {
                string cityName = req.Query["city"];
                var result = _context.Weathers.SingleOrDefault(x => x.City == cityName);
                
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}