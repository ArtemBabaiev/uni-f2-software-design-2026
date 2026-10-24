namespace BaseDotNetApp.Lecture14;

public class PolymorphismBad
{
    public enum DeliveryType { Standard, Express, International }

    public class DeliveryCalculator
    {
        // Порушення: Метод доведеться змінювати при кожному новому типі доставки
        public decimal CalculateFee(DeliveryType type, decimal weight)
        {
            return type switch
            {
                DeliveryType.Standard => weight * 1.5m,
                DeliveryType.Express => weight * 3.0m + 10m,
                DeliveryType.International => weight * 5.0m + 50m,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
    }
}

public static class Polymorphism
{
    public interface IDeliveryStrategy
    {
        decimal CalculateFee(decimal weight);
    }

    // Кожна стратегія є окремим класом (Поліморфізм)
    public class StandardDelivery : IDeliveryStrategy
    {
        public decimal CalculateFee(decimal weight) => weight * 1.5m;
    }

    public class ExpressDelivery : IDeliveryStrategy
    {
        public decimal CalculateFee(decimal weight) => weight * 3.0m + 10m;
    }

    public class OrderDeliveryService
    {
        // Поліморфна обробка без використання умовних конструкцій
        public decimal ProcessDelivery(IDeliveryStrategy strategy, decimal weight)
        {
            return strategy.CalculateFee(weight);
        }
    }
}