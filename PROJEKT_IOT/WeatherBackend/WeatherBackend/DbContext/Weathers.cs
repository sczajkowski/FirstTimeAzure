using System;
namespace WeatherBackend.DbContext{
public class Weathers
{
    public int Id { get; set; }
    public string City { get; set; }
    public decimal? Temperature { get; set; }
    public decimal? WindSpeed { get; set; }
    public int? Humidity { get; set; }
    public int Visibility { get; set; }
    public int? Clouds { get; set; }
    public int? Sunrise { get; set; }
    public int? Sunset { get; set; }
}}
