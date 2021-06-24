using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using WeatherBackend.Api;
using WeatherBackend.DbContext;
using WeatherBackend.Consts;
namespace WeatherBackend
{
    public class GetWeatherFromApi
    {
        private readonly DatabaseContext _context;
        private readonly RestClient _client;
        public GetWeatherFromApi(DatabaseContext context)
        {
            _context = context;
            _client = new RestClient("http://api.openweathermap.org/data/2.5");
        }
        [FunctionName("GetWeatherFromApi")]
        public async Task RunAsync([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
            try
            {
                foreach (var city in Consts.Consts.Cities)
                {
                    var request = new RestRequest("forecast", Method.GET);
                    request.AddParameter("q", city, ParameterType.QueryString);
                    request.AddParameter("appid", "2b35650ce7cbe72fda4a00182e2ca32d", ParameterType.QueryString);
                    request.AddParameter("units", "metric", ParameterType.QueryString);

                    var response = await _client.ExecuteAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var parsedData = JsonConvert.DeserializeObject<WeatherResponse>(response.Content);

                        if (parsedData != null)
                        {
                            var apiWeather = new Weathers()
                            {
                                City = parsedData.City.Name,
                                Temperature = parsedData.List[0].Main.Temp,
                                Humidity = parsedData.List[0].Main.Humidity,
                                WindSpeed = parsedData.List[0].Wind.Speed,
                                Visibility = parsedData.List[0].Visibility,
                                Sunrise = parsedData.City.Sunrise,
                                Sunset = parsedData.City.Sunrise,
                            };

                            var singleWeather = _context.Weathers.FirstOrDefault(x => x.City == apiWeather.City);
                            if (singleWeather == null)
                            {
                                _context.Weathers.Add(apiWeather);
                            }
                            else
                            {
                                singleWeather.Temperature = apiWeather.Temperature;
                                singleWeather.Humidity = apiWeather.Humidity;
                                singleWeather.WindSpeed = apiWeather.WindSpeed;
                                singleWeather.Visibility = apiWeather.Visibility;
                                singleWeather.Sunrise = apiWeather.Sunrise;
                                singleWeather.Sunset = apiWeather.Sunset;
                            }
                            
                        }
                        
                    }
                }

                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
            log.LogInformation($"C# Timer trigger function ends");
            
        }
    }
}