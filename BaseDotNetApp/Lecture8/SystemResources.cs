namespace BaseDotNetApp.Lecture8;

public static class SystemResources
{
    public static void Exec()
    {
        ProcessConfiguration();
    }


    private static void ProcessConfiguration()
    {
        // C# 8+ using declaration
        using (var reader =
               new SecureFileReader("/home/ababaiev/Projects/software_construction/BaseDotNetApp/BaseDotNetApp/Base.cs"))
        {
            reader.ReadData();
        }
        // DO SOMETHING ELSE;
    }

}

public class SecureFileReader : IDisposable
{
    private readonly FileStream _fileStream;

    public SecureFileReader(string path)
    {
        // Отримання некерованого ресурсу ОС
        _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
    }

    public void ReadData() => Console.WriteLine("Читання даних...");

    // Обов'язкова реалізація контракту очищення
    public void Dispose()
    {
        // 1. Закриваємо дескриптор файлу
        _fileStream?.Dispose();
        
        // 2. Відключаємо повільну фіналізацію GC
        GC.SuppressFinalize(this); 
    }
}