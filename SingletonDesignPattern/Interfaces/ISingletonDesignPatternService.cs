
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonDesignPatternServices
{
    public interface ISingletonDesignPatternService
    {
        int IncrementCounter();
        int GetCounter();
    }
}
