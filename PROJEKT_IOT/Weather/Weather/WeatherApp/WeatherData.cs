using Newtonsoft.Json;

namespace WeatherApp
{
    public class WeatherData
    {
        public string City { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? WindSpeed { get; set; }
        public int? Humidity { get; set; }
        public int Visibility { get; set; }
        public int? Clouds { get; set; }
        public long? Sunrise { get; set; }
        public long? Sunset { get; set; }
    }
}
