namespace BaseDotNetApp.Lecture11;

public static class Generics
{
    public static void Exec()
    {
        
    }
}

// Доменний контракт, який вимагає наявності ідентифікатора
public interface IIdentifiable 
{ 
    Guid Id { get; } 
}

// Узагальнений клас із суворими архітектурними обмеженнями
public class DomainRepository<T> where T : class, IIdentifiable, new()
{
    private readonly List<T> _entities = new();

    public T CreateAndRegister()
    {
        // Завдяки обмеженню new(), ми можемо інстанціювати об'єкт без рефлексії
        T newEntity = new T();
        
        // Завдяки IIdentifiable, ми можемо безпечно звертатися до властивостей контракту
        Console.WriteLine($"Зареєстровано нову сутність з ідентифікатором: {newEntity.Id}");
        
        _entities.Add(newEntity);
        return newEntity;
    }
    
    
}