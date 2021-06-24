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
    public class GetCityName
    {
        private readonly DatabaseContext _context;

        public GetCityName(DatabaseContext context)
        {
            _context = context;
        }

        [FunctionName("GetCityName")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req, ILogger log)
        {
            string city = req.Query["city"];

            var result = await _context.Weathers.Where(x => x.City.Contains(city)).Select(x=> x.City).ToListAsync();

            return new OkObjectResult(result);
        }
    }
}