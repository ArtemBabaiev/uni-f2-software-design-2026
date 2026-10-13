using System.Net.Mail;
using System.Text.Json;

namespace BaseDotNetApp.Lecture13_2.Bad;

public class SRP
{
    
}


// Антипатерн: Клас має щонайменше 3 причини для зміни (бізнес-логіка, збереження, інтеграція).
public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        // 1. Бізнес-логіка та валідація
        if (order.Items.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one item.");
        }

        var total = order.Items.Sum(i => i.Price * i.Quantity);
        if (total > 10000)
        {
            order.RequiresManagerApproval = true;
        }

        // 2. Інфраструктура: Збереження стану (Файлова система / База даних)
        var json = JsonSerializer.Serialize(order);
        File.WriteAllText($"/orders/{order.Id}.json", json);    

        // 3. Інтеграція: Надсилання повідомлень через мережу
        using var smtp = new SmtpClient("smtp.mycompany.com");
        smtp.Send("noreply@company.com", order.CustomerEmail, "Order Processed", $"Total: {total}");
    }
}

public record Order(Guid Id, string CustomerEmail, System.Collections.Generic.List<OrderItem> Items)
{
    public bool RequiresManagerApproval { get; set; }
}
public record OrderItem(string Name, decimal Price, int Quantity);