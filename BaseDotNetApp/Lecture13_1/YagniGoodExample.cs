namespace BaseDotNetApp.Lecture13_1;

public class YagniGoodExample
{
    
}

// Лаконічний, сфокусований контракт, що повністю задовольняє наявну бізнес-потребу
public interface ICustomerRepository
{
    void SaveAsync(Customer customer, CancellationToken cancellationToken = default);
}

// Конкретна реалізація для SQL Server, яка не має зайвого багажу
public class SqlCustomerRepository : ICustomerRepository
{

    public async void SaveAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        
    }
}
