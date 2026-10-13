namespace BaseDotNetApp.Lecture11;

public static class LinqPipeline
{

    public static void Exec()
    {
        List<Order> orders = GetOrders();
        // Where: фільтрація
        IEnumerable<Order> processed = orders.Where(o => o.IsProcessed);
        processed.Count();
        processed.First();
        // Select: проєкція в іншу форму
        decimal discount = 0.5m;
        IEnumerable<decimal> totals = orders.Select(o => discount * o.Price * o.Quantity); // IEnumerable<decimal>
        var enumerable = orders.Select(o => new { o.Id, o.Customer }); // анонімний тип

        var orderedEnumerable = totals.OrderBy(i => i);
        //var aggregate = totals.Aggregate();

        // OrderBy / ThenBy: сортування за кількома ключами
        IOrderedEnumerable<Order> sorted = orders
            .OrderBy(o => o.Category)
            .ThenByDescending(o => o.Price);

        // Skip / Take: пагінація (сторінка 3, по 20 елементів)
        IEnumerable<Order> page = orders.OrderBy(o => o.Id).Skip(40).Take(20);

        // SelectMany: "розгортання" колекції колекцій
        IEnumerable<string> allTags = orders.SelectMany(a => a.Items); // один плоский список предметів
    }

    public static void Process2()
    {
        List<Order> orders = GetOrders();
        // Отримання одного елемента
        Order first    = orders.First(o => o.IsProcessed);// виняток, якщо немає
        Order? maybe    = orders.FirstOrDefault(o => o.Id == 42); // null, якщо немає
        Order? unique   = orders.SingleOrDefault(o => o.Id == 42); // null, якщо немає; виняток, якщо 2+

        // Питання "так/ні"
        bool hasUnprocessed = orders.Any(o => !o.IsProcessed);
        bool allPaid        = orders.All(o => o.Price > 0);
        bool hasId          = orders.Select(o => o.Id).Contains(42);

        // Min/Max: значення проти самого елемента
        decimal maxPrice   = orders.Max(o => o.Price); // лише число
        Order   priciest   = orders.MaxBy(o => o.Price)!; // все замовлення
    }

    public static void Process3()
    {
        List<Order> orders = GetOrders();
        List<Customer> customers = GetCustomers();
        
        // Агрегати
        int     count   = orders.Count(o => o.IsProcessed);
        decimal revenue = orders.Sum(o => o.Price * o.Quantity);
        decimal avg     = orders.Average(o => o.Price);

        IEnumerable<IGrouping<string, Order>> groupBy = orders
            .GroupBy(o => o.Category);

        // GroupBy + агрегати по групах
        var report = orders
            .GroupBy(o => o.Category)
            .Select(g => new
            {
                Category = g.Key,
                Orders   = g.Count(),
                Revenue  = g.Sum(o => o.Price * o.Quantity)
            })
            .OrderByDescending(r => r.Revenue);

        // ToDictionary: пошук за ключем за O(1) (ключі мають бути унікальними!)
        Dictionary<int, Order> byId = orders.ToDictionary(o => o.Id);
        Order order42 = byId[42];

        // ToLookup: як словник, але під одним ключем може бути багато елементів
        ILookup<string, Order> byCustomer = orders.ToLookup(o => o.Customer);
        IEnumerable<Order> aliceOrders = byCustomer["Alice"];     // порожня послідовність, якщо ключа немає

        // Join: аналог SQL INNER JOIN
        var rows = orders.Join(customers,
            o => o.Customer,               // ключ із замовлень
            c => c.Name,                   // ключ із клієнтів
            (o, c) => new { c.Name, c.Email, o.Price });
    }
    
    static List<Order> GetOrders()
    {
        var orders = new List<Order>();
        return orders;
    }

    static List<Customer> GetCustomers()
    {
        var customers = new List<Customer>();
        return customers;
    }

    static void Example1()
    {
        var numbers = new List<int> { 1, 2, 3 };

        var query = numbers.Where(x => x > 1);   // нічого ще не виконано
        numbers.Add(10);                          // дані змінюються ПІСЛЯ написання запиту

        foreach (var n in query) Console.Write(n + " ");   // 2 3 10  (бачить новий елемент)
    }

//     void Example2()
//     {
//         IEnumerable<Order> expensive = db.Orders.Where(o => o.Price > 1000);  // запит, а не дані
//
//         if (expensive.Any())                        // виконує запит (1-й раз)
//         {
//             foreach (var o in expensive) { ... }    // виконує його ЗНОВУ (2-й раз)
//         }
//
// // Виправлення: матеріалізувати один раз
//         var list = db.Orders.Where(o => o.Price > 1000).ToList();
//         if (list.Count > 0)
//         {
//             foreach (var o in list) { ... }         // без другого запиту
//         }
//     }
}

public record Order(int Id, string Customer, string Category,
    decimal Price, int Quantity, bool IsProcessed, string[] Items);

public record Customer(string Name, string Email);