using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;
        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
            ListViewCities.IsVisible = false;
        }

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
                var weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint+"GetWeather", ((Button)sender).Text));
                BindingContext = new {weatherData};
                _cityEntry.Text = null;
                _cityEntry.Unfocus();
                ListViewCities.IsVisible = false;
                ScrollViewResult.IsVisible = true;
        }

        string GenerateRequestUri(string endpoint, string cityName)
        {
            string requestUri = endpoint;
            requestUri += $"?city={cityName}";
            return requestUri;
        }

        private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_cityEntry.Text != null)
            {
                
            ScrollViewResult.IsVisible = false;
                var cities =
                    await _restService.GetCities(GenerateRequestUri(Constants.OpenWeatherMapEndpoint + "GetCityName", _cityEntry.Text.Trim()));
                BindingContext = new {Cities = cities};
                ListViewCities.IsVisible = true;
            }
        }
    }
}
