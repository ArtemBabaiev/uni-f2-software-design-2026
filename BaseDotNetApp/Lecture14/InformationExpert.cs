namespace BaseDotNetApp.Lecture14;

public class InformationExpertBad
{
    // Порушення: Анемічна доменна модель (відсутність поведінки)
    public class OrderItem
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public List<OrderItem> Items { get; set; } = new();
        public decimal DiscountPercentage { get; set; }
    }

// Процедурний сервіс, який маніпулює пасивними даними
    public class OrderCalculatorService
    {
        public decimal CalculateTotal(Order order)
        {
            decimal total = 0;
        
            // Порушення інкапсуляції: сервіс "знає" занадто багато про деталі Order
            foreach (var item in order.Items)
            {
                total += item.UnitPrice * item.Quantity;
            }

            return total - (total * (order.DiscountPercentage / 100m));
        }
    }
}

public class InformationExpert
{
    // Правильне рішення: Багата доменна модель з поведінкою
    // Використання C# 12 Primary Constructors для незмінного стану
    public class OrderItem(string productId, decimal unitPrice, int quantity)
    {
        public string ProductId { get; } = productId;
        public decimal UnitPrice { get; } = unitPrice;
        public int Quantity { get; } = quantity;

        // Information Expert: Об'єкт сам знає, як обчислити свою вартість
        public decimal CalculateSubtotal() => UnitPrice * Quantity;
    }

    public class Order(decimal discountPercentage)
    {
        private readonly List<OrderItem> _items = [];
    
        // Захист колекції від зовнішніх модифікацій
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal DiscountPercentage { get; } = discountPercentage;

        public void AddItem(OrderItem item)
        {
            ArgumentNullException.ThrowIfNull(item);
            _items.Add(item);
        }

        // Information Expert: Order володіє інформацією про Items та Discount
        public decimal CalculateTotal()
        {
            decimal subtotal = 0;
        
            foreach (var item in _items)
            {
                // Делегування обов'язку експерту нижчого рівня
                subtotal += item.CalculateSubtotal();
            }

            return subtotal - (subtotal * (DiscountPercentage / 100m));
        }
    }
}
