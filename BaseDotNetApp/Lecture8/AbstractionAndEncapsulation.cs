namespace BaseDotNetApp.Lecture8;

public static class AbstractionAndEncapsulation
{
    public static void Exec()
    {
        Order order = new Order();
        
        // не можливо отримати доступ до поля
        // order._totalAmount = 10;

        // колекція доступна лише до читання
        IReadOnlyCollection<string> readOnlyCollection = order.Items;

        // setter is private
        // order.IsProcessed = true;
        var orderIsProcessed = order.IsProcessed;
    }
}

public class Order
{
    private int orderId;
    public int OrderId { get => orderId; set  => orderId = value; }
    
    // Інкапсуляція: стан прихований, модифікація ззовні неможлива
    private readonly List<string> _items = new();
    private decimal _totalAmount;

    // Абстракція: доступ на читання через властивості
    public IReadOnlyCollection<string> Items
    {
        get {return _items.AsReadOnly(); }
    }

    public decimal TotalAmount
    {
        get { return _totalAmount; }
    }

    // Властивість з модифікатором private set (може змінюватись лише всередині)
    public bool IsProcessed { get; private set; }

    public void AddItem(string itemName, decimal price)
    {
        if (IsProcessed)
        {
            throw new InvalidOperationException("Неможливо змінити оброблене замовлення.");
        }
        
        if (price < 0)
        {
            throw new ArgumentException("Ціна не може бути від'ємною.");
        }

        _items.Add(itemName);
        _totalAmount += price;
    }

    public void Process()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("Замовлення порожнє.");
        }
        IsProcessed = true;
    }
}

public class OrderVariant
{
    
    private readonly List<string> _items = new();
    public bool IsProcessed { get; private set; }
    
    public void Process()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("Замовлення порожнє.");
        }
        IsProcessed = true;
    }
}