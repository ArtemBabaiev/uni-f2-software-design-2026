namespace BaseDotNetApp.Lecture13_1;

public class DryGoodExample
{
    
}

// Використання строго типізованого переліку замість вразливих магічних рядків
public enum CustomerTier 
{ 
    Standard, 
    Premium, 
    VIP 
}

// Контракт, що визначає єдине джерело правди для підсистеми розрахунку знижок
public interface IDiscountCalculationPolicy
{
    decimal CalculateDiscountPercentage(CustomerTier tier, int loyaltyPoints);
}

// Конкретна реалізація бізнес-правила з використанням сучасного C# 9.0+ Pattern Matching
public class StandardDiscountPolicy : IDiscountCalculationPolicy
{
    public decimal CalculateDiscountPercentage(CustomerTier tier, int loyaltyPoints) => 
        (tier, loyaltyPoints) switch
        {
            (CustomerTier.VIP, _) => 0.15m,
            (CustomerTier.Premium, > 500) => 0.10m,
            _ => 0m
        };
}

public class OrderProcessingService
{
    private readonly IDiscountCalculationPolicy _discountPolicy;

    // Застосування патерну Впровадження залежностей (Dependency Injection)
    public OrderProcessingService(IDiscountCalculationPolicy discountPolicy)
    {
        _discountPolicy = discountPolicy;
    }

    public decimal CalculateTotalPrice(decimal baseAmount, CustomerTier tier, int loyaltyPoints)
    {
        var discountPercentage = _discountPolicy.CalculateDiscountPercentage(tier, loyaltyPoints);
        return baseAmount - (baseAmount * discountPercentage);
    }
}

public class InvoiceGenerationService
{
    private readonly IDiscountCalculationPolicy _discountPolicy;

    public InvoiceGenerationService(IDiscountCalculationPolicy discountPolicy)
    {
        _discountPolicy = discountPolicy;
    }

    public string GenerateFormattedInvoice(decimal baseAmount, CustomerTier tier, int loyaltyPoints)
    {
        var discountPercentage = _discountPolicy.CalculateDiscountPercentage(tier, loyaltyPoints);
        var finalAmount = baseAmount - (baseAmount * discountPercentage);
        return $"Дякуємо за покупку! До сплати: {finalAmount:C}";
    }
}