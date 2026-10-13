namespace BaseDotNetApp.Lecture10;

public static class SealedHierarchy
{
    public static void Exec()
    {
        
    }
}

// Базовий абстрактний тип задає межі алгебраїчного типу даних
public abstract record OperationResult
{
    // Приватний конструктор фізично запобігає створенню спадкоємців поза цим скоупом
    private OperationResult() { } 
    
    // Вкладені sealed класи формують вичерпний та незмінний список станів
    public sealed record Success(string DataPayload) : OperationResult;
    public sealed record Error(string ErrorCode, string Message) : OperationResult;
    public sealed record Pending(Guid CorrelationId) : OperationResult;
}


// Підхід 1: Обмеження на рівні збірки (найбільш поширений)
public abstract record DomainEvent
{
    // Завдяки модифікатору 'private protected', ієрархію можна 
    // розширювати лише в межах поточної збірки.
    private protected DomainEvent() { } 
}

public sealed record OrderPlaced(Guid OrderId, decimal Amount) : DomainEvent;
public sealed record OrderCancelled(Guid OrderId, string Reason) : DomainEvent;
public sealed record OrderRefunded(Guid OrderId, string Reason) : DomainEvent;


// Підхід 2: Суворе обмеження на рівні батьківського класу (через вкладеність)
public abstract record StrictDomainEvent
{
    // Конструктор private гарантує, що спадкоємці можуть бути
    // лише вкладеними всередині цього ж класу.
    private StrictDomainEvent() { }

    public sealed record OrderPlaced(Guid OrderId, decimal Amount) : StrictDomainEvent;
    public sealed record OrderCancelled(Guid OrderId, string Reason) : StrictDomainEvent;
}