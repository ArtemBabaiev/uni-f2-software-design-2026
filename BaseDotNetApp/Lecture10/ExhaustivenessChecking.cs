namespace BaseDotNetApp.Lecture10;

public static class ExhaustivenessChecking
{
    public static void Exec()
    {
        RouteEvent(new OrderRefunded(new Guid(), "Refund Order Reason"));
    }
    
    public static void RouteEvent(DomainEvent incomingEvent)
    {
        // Компілятор C# достовірно знає, що DomainEvent може бути 
        // ВИКЛЮЧНО OrderPlaced або OrderCancelled
        var routeTarget = incomingEvent switch
        {
            OrderPlaced { Amount: > 10000 } => "HighPriorityQueue",
            OrderPlaced => "StandardQueue",
            OrderCancelled { Reason: var r } => $"DeadLetterQueue: {r}",
            OrderRefunded => "RefundedQueue"
        };

        // Опублікувати incomingEvent
        Console.WriteLine(routeTarget);
    }
    
    public static void RouteEventStrict(StrictDomainEvent incomingEvent)
    {
        // Компілятор C# достовірно знає, що DomainEvent може бути 
        // ВИКЛЮЧНО OrderPlaced або OrderCancelled
        var routeTarget = incomingEvent switch
        {
            StrictDomainEvent.OrderPlaced { Amount: > 10000 } => "HighPriorityQueue",
            StrictDomainEvent.OrderPlaced => "StandardQueue",
            StrictDomainEvent.OrderCancelled { Reason: var r } => $"DeadLetterQueue: {r}",
            null => throw new ArgumentNullException(nameof(incomingEvent))
            // ЗВЕРНІТЬ УВАГУ: discard pattern (_) або default тут СВІДОМО ВІДСУТНІЙ.
            // Якщо до ієрархії DomainEvent додати новий тип 'OrderRefunded', 
            // компілятор негайно видасть попередження/помилку
        };
    
        // Опублікувати incomingEvent
        Console.WriteLine(routeTarget);
    }
}