namespace WeatherApp.Services
{
    using Refit;

    public interface IOpenWeatherMapApi
    {
        [Get("/forecast?lat={latitude}&lon={longitude}&units=metric")]
        Task<WeatherMapResponse> GetWeatherForecast(double latitude, double longitude);
    }

    public record WeatherMapResponse(IList<WeatherMapForecast> List);

    public record WeatherMapForecast(int Dt, WeatherMapMain Main);

    public record WeatherMapMain(double Temp);
}
