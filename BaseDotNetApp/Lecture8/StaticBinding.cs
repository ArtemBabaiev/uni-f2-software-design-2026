namespace BaseDotNetApp.Lecture8;

public static class StaticBinding
{
    public static void Exec()
    {
        // ВИКОРИСТАННЯ:
        TransactionProcessor2 processor = new CryptoTransactionProcessor2();

        // Виконається логіка TransactionProcessor (База)!
        processor.ProcessPayment(100.50m); 

        // Щоб викликати логіку Crypto, потрібне жорстке приведення типу:
        CryptoTransactionProcessor2 cryptoProcessor = new CryptoTransactionProcessor2();
        cryptoProcessor.ProcessPayment(100.50m); // Виконається логіка CryptoTransactionProcessor
    }
    
}

public class TransactionProcessor2
{
    // Звичайний метод (без virtual). 
    // Компілятор жорстко прив'язує виклик до цього типу на етапі компіляції.
    public void ProcessPayment(decimal amount) 
        => Console.WriteLine($"[База] Платіж: {amount}");
}

public class CryptoTransactionProcessor2 : TransactionProcessor2
{
    // new: приховування методу (Method Hiding), а не динамічна підміна.
    public new void ProcessPayment(decimal amount) 
        => Console.WriteLine($"[Крипто] Верифікація: {amount}");
}