using System.Text;
using System.Net;
using SingletonDesignPatternServices;
namespace SingletonDesignPatternServices
{
    public class SingletonDesignPatternService : ISingletonDesignPatternService
    {
        private int _counter = 0;

        public int IncrementCounter()
        {
            return ++_counter;
        }

        public int GetCounter()
        {
            return _counter;
        }
    }
}
