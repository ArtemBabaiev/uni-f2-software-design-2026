namespace BaseDotNetApp.Lecture14;


public class CreatorBad
{
    public class Order
    {
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem(string productId, decimal price, int quantity)
    {
        
    }
    
    public class ApplicationClient
    {
        public void AddProductToOrder(Order order, string productId, decimal price, int quantity)
        {
            // Порушення: Клієнт не є контейнером для OrderItem, 
            // але бере на себе роль Творця
            var item = new OrderItem(productId, price, quantity);
        
            // Пряма маніпуляція колекцією іншого об'єкта
            order.Items.Add(item);
        }
    }
}

public class Creator
{
    
    public class OrderItem(string productId, decimal price, int quantity)
    {
        
    }
    
    public class Order
    {
        private readonly List<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        // Патерн Creator: Order агрегує OrderItem та містить логіку їх створення
        public void CreateAndAddItem(string productId, decimal price, int quantity)
        {
            // Захисні перевірки (Guard Clauses) інкапсульовані у Творці
            if (price <= 0)
                throw new ArgumentException("Ціна повинна бути більшою за нуль.", nameof(price));
            if (quantity <= 0)
                throw new ArgumentException("Кількість повинна бути більшою за нуль.", nameof(quantity));

            var newItem = new OrderItem(productId, price, quantity);
            _items.Add(newItem);
        }
    }

    public class ApplicationClient
    {
        public void AddProductToOrder(Order order, string productId, decimal price, int quantity)
        {
            // Клієнт передає намір експерту-творцю
            order.CreateAndAddItem(productId, price, quantity);
        }
    }

}