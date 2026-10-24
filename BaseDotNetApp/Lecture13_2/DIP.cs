using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;

namespace BaseDotNetApp.Lecture13_2;

public static class DIP
{
    public static void Exec()
    {
        Bootstrapper.Run();
    }
}

public static class Bootstrapper
{
    public static void Run()
    {
        var services = new ServiceCollection();

        // Реєстрація залежностей
        services.AddScoped<IPaymentGateway, StripePaymentGateway>();
        services.AddScoped<CheckoutService>();

        var serviceProvider = services.BuildServiceProvider();

        // Контейнер сам резолвить CheckoutService,
        // ін'єктуючи StripePaymentGateway
        using var scope = serviceProvider.CreateScope();
        var checkout = scope.ServiceProvider.GetRequiredService<CheckoutService>();

        checkout.Checkout(150.00m);
    }
}

// 1. Абстракція (задовольняє DIP)
public interface IPaymentGateway
{
    void Process(decimal amount);
}

// 2. Деталь реалізації, що залежить від абстракції
public class StripePaymentGateway : IPaymentGateway
{
    public void Process(decimal amount)
    {
        // Інтеграція зі Stripe API
        Console.WriteLine($"Processed {amount} via Stripe");
    }
}

// 3. Високорівневий модуль залежить від абстракції.
// DI через Primary Constructor (C# 12)
public class CheckoutService(IPaymentGateway paymentGateway)
{
    public void Checkout(decimal totalAmount)
    {
        // Логіка не залежить від провайдера
        paymentGateway.Process(totalAmount);
    }
}