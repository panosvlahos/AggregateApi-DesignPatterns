namespace FactoryDesignPattern
{
   
        public class EmailNotification : IFactoryService
    {
            public string Send(string message)
            {
                return $"📧 Email sent: {message}";
            }
        }

        public class SmsNotification : IFactoryService
    {
            public string Send(string message)
            {
                return $"📱 SMS sent: {message}";
            }
        }

        public class PushNotification : IFactoryService
    {
            public string Send(string message)
            {
                return $"📢 Push notification sent: {message}";
            }
        }
    
}
