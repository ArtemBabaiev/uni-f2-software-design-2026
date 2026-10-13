using System.Reflection;

namespace BaseDotNetApp.Lecture12;

public class DynamicReflections
{
    public static void Exec()
    {
        UserProfile user = (UserProfile) DynamicObjectManipulator.CreateAndConfigureUser();
        Console.WriteLine(user.Username);   
    }
}

public static class DynamicObjectManipulator
{
    public static object? CreateAndConfigureUser()
    {
        // Отримання метаданих цільового типу
        Type targetType = typeof(UserProfile);

        // Динамічне створення екземпляра класу
        // Цей виклик звертається до конструктора за замовчуванням
        object? instance = Activator.CreateInstance(targetType);

        if (instance != null)
        {
            // Пошук властивості за іменем
            PropertyInfo? usernameProp = targetType.GetProperty(nameof(UserProfile.Username));
            
            // Динамічне встановлення значення, незважаючи на те, що
            // змінна instance має тип object
            usernameProp?.SetValue(instance, "Dynamic_Administrator");
            Console.WriteLine((string)usernameProp.GetValue(instance));
        }

        return instance;
    }
}