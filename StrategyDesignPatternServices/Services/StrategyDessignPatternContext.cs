using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyDesignPatternServices.Services
{
    public  class StrategyDessignPatternContext
    {
      
            private IStrategyDesignPatternService _paymentStrategy;

            public void SetPaymentStrategy(IStrategyDesignPatternService paymentStrategy)
            {
                _paymentStrategy = paymentStrategy;
            }

            public string ProcessPayment(decimal amount)
            {
                if (_paymentStrategy == null)
                {
                    throw new InvalidOperationException("Payment strategy not set.");
                }
                return _paymentStrategy.ProcessPayment(amount);
            }
      
    }
}
