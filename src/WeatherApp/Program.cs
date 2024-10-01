using Microsoft.Extensions.Options;
using Refit;
using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();

builder.Services.Configure<WeatherMapConfiguration>(builder.Configuration.GetSection("WeatherMap"));
builder.Services.AddSingleton<IValidateOptions<WeatherMapConfiguration>, WeatherMapConfigurationValidator>();
builder.Services.AddTransient<ApiKeyHandler>();
builder.Services.AddRefitClient<IOpenWeatherMapApi>()
    .ConfigureHttpClient((provider, client) =>
    {
        var configuration = provider.GetRequiredService<IOptions<WeatherMapConfiguration>>().Value;
        client.BaseAddress = new Uri(configuration.Uri);
    })
    .AddHttpMessageHandler<ApiKeyHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
