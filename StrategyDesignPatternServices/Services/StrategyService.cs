
namespace StrategyDesignPatternServices
{
    public class CreditCardPayment : IStrategyDesignPatternService
    {
        public string ProcessPayment(decimal amount)
        {
            return $"Paid {amount:C} using Credit Card.";
        }
    }

    public class PayPalPayment : IStrategyDesignPatternService
    {
        public string ProcessPayment(decimal amount)
        {
            return $"Paid {amount:C} using PayPal.";
        }
    }

    public class BankTransferPayment : IStrategyDesignPatternService
    {
        public string ProcessPayment(decimal amount)
        {
            return $"Paid {amount:C} using Bank Transfer.";
        }
    }
}
