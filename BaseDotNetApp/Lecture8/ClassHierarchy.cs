namespace BaseDotNetApp.Lecture8;

public static class ClassHierarchy
{
    public static void Exec()
    {
        
    }
}

// Базовий клас з використанням Primary Constructor
public class Employee(string id, string name)
{
    public string Id { get; } = id;
    public string Name { get; } = name;
}

// Похідний клас розширює базовий
public class SoftwareEngineer : Employee
{
    public string TechStack { get; }

    // Конструктор нащадка делегує ініціалізацію базовому через : base()
    public SoftwareEngineer(string id, string name, string techStack) 
        : base(id, name)
    {
        if (string.IsNullOrWhiteSpace(techStack))
            throw new ArgumentException("Стек не вказано");
            
        TechStack = techStack;
    }
}