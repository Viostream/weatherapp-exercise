using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherApp.Services;

namespace WeatherApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWeatherService _weatherService;
    public WeatherForecast? Today { get; set; }
    public WeatherForecast? Yesterday { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    public async Task<IActionResult> OnGet(WeatherForecast[]? forecasts)
    {
        var result = await _weatherService.GetWeatherForecasts();
        if (result.Length > 0)
        {
            Yesterday = result[0];
        }

        return Page();
    }
}
