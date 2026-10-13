using System.Collections.Generic;
using System.Linq;
using BaseDotNetApp.Lecture13_2.Bad;

namespace BaseDotNetApp.Lecture13_2;

public class OCP
{
    
}



// 1. Визначення проєктної точки розширення (Extension Point)
public interface IDiscountStrategy
{
    bool IsMatch(CustomerType type);
    decimal ApplyDiscount(decimal amount);
}

// 2. Реалізація конкретних стратегій у нових файлах.
// Модифікація існуючого коду не потрібна.
public class PremiumDiscountStrategy : IDiscountStrategy
{
    public bool IsMatch(CustomerType type) => type == CustomerType.Premium;
    public decimal ApplyDiscount(decimal amount) => amount * 0.9m;
}

public class VipDiscountStrategy : IDiscountStrategy
{
    public bool IsMatch(CustomerType type) => type == CustomerType.Vip;
    public decimal ApplyDiscount(decimal amount) => amount * 0.8m;
}

public class StandardDiscountStrategy : IDiscountStrategy
{
    public bool IsMatch(CustomerType type) => type == CustomerType.Standard;
    public decimal ApplyDiscount(decimal amount) => amount;
}

// 3. Контекст, закритий для модифікацій, але відкритий для розширення.
// Використовується C# 12 Primary Constructor для ін'єкції колекції стратегій.
public class DiscountCalculator(IEnumerable<IDiscountStrategy> strategies)
{
    public decimal CalculateDiscount(decimal amount, CustomerType type)
    {
        // Сучасний підхід до пошуку відповідної поведінки
        var strategy = strategies.FirstOrDefault(s => s.IsMatch(type)) 
                       ?? throw new InvalidOperationException($"No strategy found for {type}");
                       
        return strategy.ApplyDiscount(amount);
    }
}