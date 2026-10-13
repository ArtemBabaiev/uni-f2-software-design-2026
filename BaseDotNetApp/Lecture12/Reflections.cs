using System.Reflection;

namespace BaseDotNetApp.Lecture12;

public class Reflections
{
    public static void Exec()
    {
        //TypeInspector.InspectType<TestUser>();
        TestUser testUser = new TestUser();
        Type type = typeof(TestUser);
        var methodInfo = type.GetMethod(nameof(TestUser.CalculateHash));
        methodInfo.Invoke(testUser, new object[] { "TestUser",  12});
    }
}

public class TestUser
{
    private int _id;
    protected string _passwordHash;
    public string Username { get; set; }

    public void CalculateHash(string salt, int iterations)
    {
        Console.WriteLine($"Inside hash function {salt}; {iterations}");
    }
    public static bool ValidateUser(TestUser user) { return true; }
}


public static class TypeInspector
{
    // Головний метод, який об'єднує всі перевірки
    public static void InspectType<T>()
    {
        Console.WriteLine($"=== Повний аналіз типу: {typeof(T).FullName} ===\n");
        InspectTypeInfo<T>();
        InspectTypeProperties<T>();
        InspectTypeFields<T>();
        InspectTypeMethods<T>();
    }

    // Окремий метод для отримання загальної інформації про клас
    public static void InspectTypeInfo<T>()
    {
        Type type = typeof(T);

        type.GetMethod(nameof(TestUser.ValidateUser));
        
        Console.WriteLine("[Загальна інформація]");
        Console.WriteLine($"Простір імен: {type.Namespace}");
        Console.WriteLine($"Базовий клас: {type.BaseType?.FullName ?? "Немає (або Object)"}");
        Console.WriteLine($"Чи є класом: {type.IsClass}");
        Console.WriteLine($"Чи є абстрактним: {type.IsAbstract}");
        Console.WriteLine($"Чи є запечатаним (sealed): {type.IsSealed}");
        Console.WriteLine($"Чи є інтерфейсом: {type.IsInterface}");
        Console.WriteLine();
    }

    // Окремий метод для аналізу властивостей
    public static void InspectTypeProperties<T>()
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | 
                                                       BindingFlags.Instance | BindingFlags.Static);
        
        Console.WriteLine("[Властивості (всі рівні доступу)]");
        foreach (PropertyInfo prop in properties)
        {
            Console.WriteLine($"- {prop.PropertyType.Name} {prop.Name}");
        }
        Console.WriteLine();
    }

    // Окремий метод для аналізу полів
    public static void InspectTypeFields<T>()
    {
        Type type = typeof(T);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | 
                                            BindingFlags.Instance | BindingFlags.Static);
        
        Console.WriteLine("[Поля]");
        foreach (FieldInfo field in fields)
        {
            // Визначаємо модифікатори доступу та статики
            string accessModifier = GetAccessModifier(field.IsPublic, field.IsPrivate, field.IsFamily, field.IsAssembly);
            string staticModifier = field.IsStatic ? "static " : "";
            
            Console.WriteLine($"- {accessModifier} {staticModifier}{field.FieldType.Name} {field.Name}");
        }
        Console.WriteLine();
    }

    // Окремий метод для аналізу методів (+ параметри)
    public static void InspectTypeMethods<T>()
    {
        Type type = typeof(T);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | 
                                               BindingFlags.Instance | BindingFlags.Static);
        
        Console.WriteLine("[Методи]");
        foreach (MethodInfo method in methods)
        {
            // Визначаємо модифікатори доступу та статики
            string accessModifier = GetAccessModifier(method.IsPublic, method.IsPrivate, method.IsFamily, method.IsAssembly);
            string staticModifier = method.IsStatic ? "static " : "";

            // Отримання масиву параметрів методу та їх форматування
            ParameterInfo[] parameters = method.GetParameters();
            string paramString = string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));

            Console.WriteLine($"- {accessModifier} {staticModifier}{method.ReturnType.Name} {method.Name}({paramString})");
        }
        Console.WriteLine();
    }

    // Допоміжний метод для перетворення логічних прапорців у текстовий модифікатор доступу
    private static string GetAccessModifier(bool isPublic, bool isPrivate, bool isFamily, bool isAssembly)
    {
        if (isPublic) return "public";
        if (isPrivate) return "private";
        if (isFamily) return "protected";
        if (isAssembly) return "internal";
        return "protected internal";
    }
}