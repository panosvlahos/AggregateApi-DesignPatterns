using AutoMapper;
using Domain.Models.Response;
using ApiDtos.Response;
using ApiDtos.Request;
using Domain.Models.Request;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        
        CreateMap<NewsApiResponse, NewsDTO>();
        CreateMap<NewsApiResponse.Article, NewsDTO.Article>()
            .ForMember(dest => dest.source, opt => opt.MapFrom(src => src.source)); 
        CreateMap<NewsApiResponse.Source, NewsDTO.Source>();

        CreateMap<OpenWeatherMapResponse, WeatherDTO>();
        CreateMap<Domain.Models.Response.Weather, ApiDtos.Response.Weather>();
        CreateMap<Domain.Models.Response.Main, ApiDtos.Response.Main>();
        CreateMap<Domain.Models.Response.Wind, ApiDtos.Response.Wind>();
        CreateMap<Domain.Models.Response.Clouds, ApiDtos.Response.Clouds>();
        CreateMap<Domain.Models.Response.Sys, ApiDtos.Response.Sys>();
        CreateMap<Domain.Models.Response.Coord, ApiDtos.Response.Coord>();
        







        CreateMap<TwitterResponse, TwitterDTO>();
        CreateMap<Domain.Models.Response.TweetData, ApiDtos.Response.TweetData>();
        CreateMap<Domain.Models.Response.Includes, ApiDtos.Response.Includes>();
        CreateMap<Domain.Models.Response.User, ApiDtos.Response.User>();
        CreateMap<Domain.Models.Response.Meta, ApiDtos.Response.Meta>();


        CreateMap<NewsInfoRequest, NewsApiRequest>();
        CreateMap<OpenWeatherRequest, OpenWeatherMapRequest>();
    }
}
