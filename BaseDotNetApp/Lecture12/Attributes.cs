namespace BaseDotNetApp.Lecture12;

public static class Attributes
{
    public static void Exec()
    {
        
    }
    
}

// Обмеження використання атрибута лише для властивостей (Property).
// AllowMultiple = false забороняє застосовувати атрибут кілька разів до одного елемента.
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class StringLengthValidatorAttribute : Attribute
{
    public int MaxLength { get; }

    // Конструктор атрибута визначає обов'язкові параметри метаданих
    public StringLengthValidatorAttribute(int maxLength)
    {
        if (maxLength <= 0) throw new ArgumentOutOfRangeException(nameof(maxLength));
        MaxLength = maxLength;
    }
    
}

// Використання сучасного синтаксису записів (records) для доменних моделей
public record UserProfile
{
    // Декларативне додавання метаданих до властивості
    [StringLengthValidator(20)]
    public required string Username { get; init; }

    [StringLengthValidator(250)]
    public string? Biography { get; init; }
}