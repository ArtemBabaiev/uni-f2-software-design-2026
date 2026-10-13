using System.Reflection;

namespace BaseDotNetApp.Lecture12;

public class AtttributeEngine
{
    public static void Exec()
    {
        UserProfile profile = new UserProfile() {Biography = "akjsdflkafhklasdj lka skfjasldfk ", Username = "asmj ddsak"};
        ValidationEngine.ValidateEntity(profile);
    }
}

public static class ValidationEngine
{
    static Dictionary<Type, List<PropertyInfo>> cache = new Dictionary<Type, List<PropertyInfo>>();
    
    public static bool ValidateEntity(object entity)
    {
        Type type = entity.GetType(); // 1. Отримуємо тип
        if (!cache.ContainsKey(type))
        {
            List<PropertyInfo> propertyInfosList = new List<PropertyInfo>();
            foreach (var prop in type.GetProperties())
            {
                IEnumerable<StringLengthValidatorAttribute> attributes = prop.GetCustomAttributes<StringLengthValidatorAttribute>();
                if (attributes.Any())
                {
                    propertyInfosList.Add(prop); 
                }
            }
            
            cache.Add(type, propertyInfosList);
        }
        List<PropertyInfo> propertyInfos = cache[type];
        
        foreach (PropertyInfo prop in propertyInfos) // 2. Ітеруємо властивості
        {
            // 3. Здійснюємо пошук нашого атрибута
            var attribute = prop.GetCustomAttribute<StringLengthValidatorAttribute>(false);
            
            if (attribute != null)
            {
                // 4. Динамічно отримуємо фактичне значення
                string? actualValue = prop.GetValue(entity) as string;
                
                // 5. Виконуємо бізнес-правило
                if (actualValue != null && actualValue.Length > attribute.MaxLength)
                {
                    Console.WriteLine($"[Помилка] '{prop.Name}' перевищує ліміт {attribute.MaxLength}.");
                    return false;
                }
            }
        }
        return true;
    }
}