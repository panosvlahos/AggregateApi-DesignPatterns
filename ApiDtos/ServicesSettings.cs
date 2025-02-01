namespace ApiDtos
{

    public class ServicesSettings
    {
        public OpenWeatherApiConfig OpenWeatherApi { get; set; }
        public NewsApiConfig NewsApi { get; set; }
        public TwitterConfig Twitter { get; set; }
    }

    public class OpenWeatherApiConfig
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }
    }

    public class NewsApiConfig
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }
    }

    public class TwitterConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Url { get; set; }
        public string UrlAuth { get; set; }
    }

}
