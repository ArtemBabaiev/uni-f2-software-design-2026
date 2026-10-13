namespace BaseDotNetApp.Lecture8;

public static class SealedClasses
{
    public static void Exec()
    {
        
    }
}

// Інтерфейси описують винятково контракти поведінки
public interface IAuditable { void WriteAuditLog(string eventName); }
public interface IAccessController { bool ValidateAccess(string userId); }

// Запечатаний клас реалізує множинні контракти без Diamond Problem
public sealed class SecurityManager : IAuditable, IAccessController
{
    private readonly string _systemKey;
    public SecurityManager(string key) => _systemKey = key;

    public void WriteAuditLog(string eventName) { /* Логіка аудиту */ }

    public bool ValidateAccess(string userId)
    {
        // Критична логіка, яку неможливо змінити через успадкування
        return !string.IsNullOrEmpty(userId) && _systemKey != null;
    }
}