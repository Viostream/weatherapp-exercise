namespace WeatherApp.Services
{

    public interface IWeatherService
    {
        Task<WeatherForecast[]> GetWeatherForecasts();
    }

    public class OpenWeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapApi _openWeatherMapApi;
        private static readonly (double Latitude, double Longitude) Coordinates = (13.067439, 80.237617);

        public OpenWeatherService(IOpenWeatherMapApi openWeatherMapApi)
        {
            _openWeatherMapApi = openWeatherMapApi;
        }

        public async Task<WeatherForecast[]> GetWeatherForecasts()
        {
            var weatherApiResponse = await _openWeatherMapApi.GetWeatherForecast(Coordinates.Latitude, Coordinates.Longitude);

            var computeWeatherSummary = (double temperature) =>
                temperature switch
                {
                    < 0 => "Freezing",
                    >= 0 and < 5 => "Bracing",
                    >= 5 and < 12 => "Chilly",
                    >= 12 and < 18 => "Cool",
                    >= 18 and < 24 => "Mild",
                    >= 24 and < 30 => "Warm",
                    >= 30 and < 35 => "Balmy",
                    >= 35 and < 40 => "Hot",
                    >= 40 and < 45 => "Sweltering",
                    >= 45 => "Scorching",
                    _ => "Warm"
                };
            return weatherApiResponse.List
                .Select(x =>
                    new WeatherForecast
                    {
                        Location = "Chennai",
                        Date = DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(x.Dt).Date),
                        TemperatureC = Convert.ToInt32(x.Main.Temp),
                        Summary = computeWeatherSummary(x.Main.Temp)
                    })
                .ToArray();
        }
    }
}
