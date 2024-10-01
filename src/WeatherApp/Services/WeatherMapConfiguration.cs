using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Services;

public class WeatherMapConfiguration
{
    [Required]
    public required string ApiKey { get; init; }

    [Required]
    [Url]
    public required string Uri { get; init; }

}

[OptionsValidator]
public partial class WeatherMapConfigurationValidator : IValidateOptions<WeatherMapConfiguration>
{

}