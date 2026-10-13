namespace BaseDotNetApp.Lecture13_2.Bad;

public class OCPBadExample
{
    
}

public enum CustomerType { Standard, Premium, Vip }

public class DiscountCalculator
{
    public decimal CalculateDiscount(decimal amount, CustomerType type)
    {
        // Порушення OCP: Додавання нового типу клієнта (напр., Employee)
        // вимагає модифікації цього стабільного методу.
        return type switch
        {
            CustomerType.Standard => amount,
            CustomerType.Premium => amount * 0.9m,
            CustomerType.Vip => amount * 0.8m,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}