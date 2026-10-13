namespace BaseDotNetApp.Lecture9;

public static class Records
{
    public static void Exec()
    {
        var originalEvent = new PaymentProcessedEvent(Guid.NewGuid(), 100.00m, "USD");
            
        // Замість зміни стану існуючого об'єкта, створюється новий зі зміненим полем Amount.
        // // Це ключовий патерн у Data-Oriented Programming для забезпечення потокобезпечності.
        var modifiedEvent = originalEvent with { Amount = 150.00m };

        // Порівняння за значенням: створюємо точний клон (змінюючи 0 полів)
        var clonedEvent = modifiedEvent with { };
        // Виведе True, оскільки порівнюються значення полів, а не посилання в пам'яті
        Console.WriteLine(modifiedEvent == clonedEvent);
        
        PaymentProcessedEvent ppEventt = new PaymentProcessedEvent(Guid.NewGuid(), 5.0m, "UAH");
        PaymentProcessedEvent event2 = ppEventt with { Amount = 5.0m, Currency = "UAH" };
        Console.WriteLine(ppEventt == event2);
        Console.WriteLine(ppEventt.Equals(event2));
    }
}

// Компілятор Roslyn автоматично генерує:
// // 1. Властивості init-only для кожного параметра.
// // 2. Методи Equals(), GetHashCode(), ToString().
// // 3. Метод Deconstruct() для розкладання на змінні.
public record PaymentProcessedEvent(Guid TransactionId, decimal Amount, string Currency)
{
    public decimal convertToCurrency(String currencyCode)
    {
        return Amount * 2.5m;
    }
};