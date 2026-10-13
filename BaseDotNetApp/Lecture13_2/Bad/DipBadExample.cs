using Microsoft.Data.SqlClient;

namespace BaseDotNetApp.Lecture13_2.Bad;

public class DipBadExample
{
    
}


public class PaymentService
{
    private readonly SqlConnection _connection;

    public PaymentService()
    {
        // Порушення DIP: високорівневий сервіс
        // створює низькорівневий ресурс
        _connection = new SqlConnection("Server=myServer;Database=myDb;");
    }

    public async Task ProcessPaymentAsync()
    {
        // Взаємодія з БД через _connection
        await Task.Delay(100);
    }
}