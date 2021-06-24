using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherBackend;
using WeatherBackend.DbContext;

[assembly: FunctionsStartup(typeof(Startup))]

namespace WeatherBackend
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var sqlConnection = Environment.GetEnvironmentVariable("SQLConnection");
            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(sqlConnection));
        }
    }
}