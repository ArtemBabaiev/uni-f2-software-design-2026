namespace BaseDotNetApp.Lecture13_2;

public class SRP
{
    
}

// Використання Primary Constructors (C# 12) для лаконічної ініціалізації залежностей.
// Клас OrderProcessor тепер є виключно оркестратором (Application Layer).
public class OrderProcessor(
    IOrderValidator validator,
    IOrderRepository repository,
    INotificationService notificationService)
{
    public async Task ProcessOrderAsync(Order order, CancellationToken ct = default)
    {
        // 1. Делегування перевірок спеціалізованому валідатору
        validator.Validate(order);

        // 2. Виконання багатої бізнес-логіки, інкапсульованої в самій сутності домену
        order.EvaluateApprovalRequirement();

        // 3. Абстрагування механізму збереження
         repository.Save(order, ct);

        // 4. Абстрагування механізму інтеграції
         notificationService.NotifyCustomer(order, ct);
    }
}

// Контракти для інфраструктурних завдань
public interface IOrderValidator { void Validate(Order order); }
public interface IOrderRepository { void Save(Order order, CancellationToken ct); }
public interface INotificationService { void NotifyCustomer(Order order, CancellationToken ct); }

// Сутність домену (Багата модель, що інкапсулює власний стан та поведінку)
public class Order
{
    // Використання init-only властивостей для безпечного створення об'єкта
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string CustomerEmail { get; init; }
    
    // Використання Collection Expressions (C# 12) для ініціалізації порожнього списку
    public List<OrderItem> Items { get; init; } = []; 
    public bool RequiresManagerApproval { get; private set; }

    // Бізнес-правила живуть всередині самої сутності
    public void EvaluateApprovalRequirement()
    {
        var total = Items.Sum(i => i.Price * i.Quantity);
        RequiresManagerApproval = total > 10000;
    }
}

// Records забезпечують незмінність та порівняння за значенням для DTO/Value Objects
public record OrderItem(string Name, decimal Price, int Quantity);