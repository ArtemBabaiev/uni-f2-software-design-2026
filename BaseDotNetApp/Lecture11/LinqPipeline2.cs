namespace BaseDotNetApp.Lecture11;

public class LinqPipeline2
{
    private readonly List<WarehouseOrder> _orders = new()
    {
        new("Електроніка", 1200.00, 1, true),
        new("Електроніка", 300.00, 3, false),
        new("Офіс", 15.50, 50, true),
        new("Меблі", 850.00, 2, true)
    };

    public void AnalyzeData()
    {
        // 1. Класичний конвеєр з лінивим виконанням
        // Використовується static для запобігання замиканням та алокаціям <>c__DisplayClass
        var processedElectronics = _orders
            .Where(static o => o.Category == "Електроніка" && o.IsProcessed)
            .Select(static o => o.Price * o.Quantity);
            
        // Запит виконується фізично лише під час виклику термінального методу Sum()
        double totalElectronicsRevenue = processedElectronics.Sum();
        Console.WriteLine($"Прибуток з електроніки: {totalElectronicsRevenue}");

        // 2. Нововведення .NET 9: метод CountBy
        // Рахує кількість замовлень за категоріями без створення проміжних Grouping колекцій
        IEnumerable<KeyValuePair<string, int>> countByCategory = _orders.CountBy(static o => o.Category);
        
        foreach (var (category, count) in countByCategory)
        {
            Console.WriteLine($"Категорія: {category}, Кількість замовлень: {count}");
        }

        // 3. Нововведення .NET 9: метод AggregateBy
        // Обчислює сумарну вартість товарів за категоріями за один високоефективний прохід
        var revenueByCategory = _orders.AggregateBy(
            keySelector: static o => o.Category,
            seed: 0.0, // Початкове значення акумулятора
            func: static (total, o) => total + (o.Price * o.Quantity) // Акумулятивна логіка
        );

        // 4. Нововведення .NET 9: метод Index()
        // Видає елемент разом з його позицією, без використання сторонніх лічильників
        foreach (var (index, order) in _orders.Index())
        {
            Console.WriteLine($"Рядок {index} - Категорія: {order.Category}");
        }
    }
}


public record WarehouseOrder(string Category, double Price, int Quantity, bool IsProcessed);
