using AggregationAPI.Models;
using ApiDtos;
using Application.Interface;
using Application.Services;
using CacheServices;
using FactoryDesignPatternServices.Services;
using NewsApiServices;
using OpenWeatherMapServices;
using SingletonDesignPatternServices;
using StatisticServices;
using StrategyDesignPatternServices.Services;
using TwitterServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.Configure<ServicesSettings>(builder.Configuration.GetSection("Services"));
//builder.Services.AddScoped<ICacheService, CacheService.Services.CacheService>();
//builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddSingleton<ISingletonDesignPatternService, SingletonDesignPatternService>();

builder.Services.AddScoped<FactoryDesignPatternService>();
builder.Services.AddScoped<StrategyDessignPatternContext>();


builder.Services.AddHttpClient<INewsApiService, NewsApiService>();
builder.Services.AddScoped<INewsApiService, NewsApiService>();

builder.Services.AddHttpClient<IOpenWeatherMapService, OpenWeatherMapService>();
builder.Services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();

builder.Services.AddHttpClient<ITwitterServiceService, TwitterService>();
builder.Services.AddScoped<ITwitterServiceService, TwitterService>();

builder.Services.AddScoped<ICollectData,CollectData>();

builder.Services.AddSingleton<ICacheService, CacheService>();


builder.Services.AddScoped<IStatisticService, StatisticService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseMiddleware<TokenMiddleware>(); // TokenMiddleware is added here


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
