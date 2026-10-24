namespace BaseDotNetApp.Lecture14;

public class Indirection
{
    // Зовнішній компонент (API банку), який неможливо змінити
    public class MonolithicBankGateway
    {
        public bool ProcessTransaction(decimal amount) => true;
    }

    // Indirection: проміжний інтерфейс
    public interface IPaymentProvider
    {
        bool Pay(decimal amount);
    }

    // Адаптер: реалізація посередника
    public class BankAdapter(MonolithicBankGateway gateway) : IPaymentProvider
    {
        private readonly MonolithicBankGateway _gateway = gateway;

        public bool Pay(decimal amount)
        {
            // Посередник узгоджує інтерфейси та згладжує несумісності
            return _gateway.ProcessTransaction(amount);
        }
    }

    // Клієнт захищений від прямого зв'язку з банком
    public class CheckoutProcessor(IPaymentProvider paymentProvider)
    {
        private readonly IPaymentProvider _paymentProvider = paymentProvider;

        public void Complete(decimal amount)
        {
            _paymentProvider.Pay(amount);
        }
    }
}