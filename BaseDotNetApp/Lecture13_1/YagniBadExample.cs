namespace BaseDotNetApp.Lecture13_1;

public class YagniBadExample
{
    
}

// Громіздкий інтерфейс передбачає збереження в SQL, NoSQL, XML та хмарні сховища
// хоча поточна бізнес-вимога — це звичайне збереження у локальну базу даних
public interface IUniversalCustomerStorageRepository<T> where T : class
{
    void SaveToSqlDatabase(T entity);
    void ExportToMongoDb(T entity); // Створено: "на випадок міграції на NoSQL"
    void SerializeToCustomXmlFile(T entity); // Створено: "можливо знадобиться для інтеграції"
    void UploadToAzureBlobStorage(T entity); // Створено: "для майбутньої хмарної архітектури"
    void CompressDataAndArchive(T entity); // Створено: "для гіпотетичного бекапу"
}

public class CustomerDataStorageRepository : IUniversalCustomerStorageRepository<Customer>
{
    public void SaveToSqlDatabase(Customer entity)
    {
        // Реальний, робочий код використання Entity Framework Core...
        Console.WriteLine("SAved to SQl database");
    }

    // Величезна кількість нереалізованого функціоналу "на майбутнє", 
    // що відверто порушує принцип підстановки Лісков (Liskov Substitution Principle)
    public void ExportToMongoDb(Customer entity) 
        => throw new  ("Функціонал буде додано у версії 2.0");
    
    public void SerializeToCustomXmlFile(Customer entity) 
        => throw new NotImplementedException("Очікуємо підтвердження формату XML");
    
    public void UploadToAzureBlobStorage(Customer entity) 
        => throw new NotImplementedException("Потрібно отримати ключі доступу до Azure");
    
    public void CompressDataAndArchive(Customer entity) 
        => throw new NotImplementedException("Алгоритм стиснення ще не обрано");
}

public class Customer
{
    
}