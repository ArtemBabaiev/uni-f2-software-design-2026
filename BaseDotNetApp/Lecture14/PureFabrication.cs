namespace BaseDotNetApp.Lecture14;

public class PureFabricationBad
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Порушення SRP: Доменна сутність керує інфраструктурою
        public void SaveToDatabase(string connectionString)
        {
            // SQL-запити та підключення до БД...
        }
    }
}

public static class PureFabrication
{
    public class Order
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public decimal TotalAmount { get; set; }
    }

    // Pure Fabrication: Сервіс, створений штучно для збереження Low Coupling
    public class OrderRepository
    {
        public void Save(Order order)
        {
            // Технічна логіка збереження ізольована від домену
            Console.WriteLine($"Замовлення {order.Id} збережено у сховище.");
        }
    }
}