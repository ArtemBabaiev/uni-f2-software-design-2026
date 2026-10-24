namespace BaseDotNetApp.Lecture14;

public class ProtectedVariations
{
    // Точка варіативності: Зовнішній алгоритм розрахунку податків
    public record TaxRequest(string RegionCode, decimal Amount);

    // Стабільна абстракція (Захисний бар'єр Protected Variations)
    public interface ITaxCalculator
    {
        decimal CalculateTax(TaxRequest request);
    }

    // Реалізація може вільно еволюціонувати без впливу на CheckoutService
    public class DefaultTaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(TaxRequest request) => request.Amount * 0.20m;
    }

    // Ядро системи захищене від змін
    public class CheckoutService(ITaxCalculator taxCalculator)
    {
        private readonly ITaxCalculator _taxCalculator = taxCalculator;

        public decimal FinalizeCheckout(string region, decimal amount)
        {
            var request = new TaxRequest(region, amount);
        
            // Виклик стабільного інтерфейсу замість жорсткої логіки
            decimal tax = _taxCalculator.CalculateTax(request);
            return amount + tax;
        }
    }
}