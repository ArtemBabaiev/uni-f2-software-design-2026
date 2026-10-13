namespace BaseDotNetApp.Lecture8;

public static class AbstractClassAndInterface
{
    public static void Exec()
    {
        
    }
}

// Інтерфейс: вимагає поведінку (CAN-DO) від будь-якого об'єкта
public interface IFormattableData { string FormatOutput(); }

// Абстрактний клас: задає базу та інкапсулює стан (IS-A)
public abstract class ReportBase : IFormattableData
{
    protected readonly string Title; // Стан
    protected ReportBase(string title) => Title = title; 

    // Абстрактний метод: нащадок ЗОБОВ'ЯЗАНИЙ надати логіку
    protected abstract string GenerateContent();

    public string FormatOutput() => $"=== {Title.ToUpper()} ===\n{GenerateContent()}";
}

// Конкретна реалізація
public class SalesReport : ReportBase
{
    public SalesReport(string title) : base(title) { }
    protected override string GenerateContent() => "Дані про продажі: 100 од.";
}