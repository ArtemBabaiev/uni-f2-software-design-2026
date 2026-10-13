namespace BaseDotNetApp.Lecture8;

public static class DynamicBinding
{
    public static void Exec()
    {
        // ВИКОРИСТАННЯ:
        TransactionProcessor processor = new CryptoTransactionProcessor();

        // Виконається логіка CryptoTransactionProcessor
        processor.ProcessPayment(100.50m); 

        // Щоб викликати логіку Crypto, потрібне жорстке приведення типу:
        CryptoTransactionProcessor cryptoProcessor = new CryptoTransactionProcessor();
        cryptoProcessor.ProcessPayment(100.50m); // Виконається логіка CryptoTransactionProcessor
    }
}

public class TransactionProcessor
{
    // virtual: метод відкритий для перевизначення
    public virtual void ProcessPayment(decimal amount) 
        => Console.WriteLine($"[База] Платіж: {amount}");
}

public class CryptoTransactionProcessor : TransactionProcessor
{
    // override: динамічна підміна поведінки
    public override void ProcessPayment(decimal amount) 
        => Console.WriteLine($"[Крипто] Верифікація: {amount}");
}