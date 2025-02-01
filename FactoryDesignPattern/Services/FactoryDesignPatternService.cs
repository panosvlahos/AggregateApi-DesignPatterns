using FactoryDesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDesignPatternServices.Services
{
    public class FactoryDesignPatternService
    {
        public static IFactoryService CreateNotification(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailNotification(),
                "sms" => new SmsNotification(),
                "push" => new PushNotification(),
                _ => throw new ArgumentException("Invalid notification type"),
            };
        }
    }
}
