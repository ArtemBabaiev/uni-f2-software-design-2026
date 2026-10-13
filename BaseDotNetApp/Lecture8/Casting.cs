namespace BaseDotNetApp.Lecture8;

public static class Casting
{
    public static void Exec()
    {
        object incomingData = GetShapeFromNetwork();

        // 1. Застарілий підхід (оператор as + перевірка на null)
        var oldCircle = incomingData as Circle;
        if (oldCircle != null)
        {
            Console.WriteLine($"Радіус: {oldCircle.Radius}");
        }

        // 2. Сучасний стандарт C# (Declaration Pattern)
        // Перевірка та приведення об'єднані в одну операцію
        if (incomingData is Circle { Radius: > 0 } circle )
        {
            Console.WriteLine($"Радіус: {circle.Radius}");
        }

        // 3. Property Pattern + Logical Patterns
        // Одночасна перевірка типу та значень його властивостей
        if (incomingData is Rectangle { Width: > 10, Height: > 10 } largeRect)
        {
            Console.WriteLine($"Великий прямокутник: {largeRect.Width}x{largeRect.Height}");
        }

        // 4. Сучасні switch expressions з Discard pattern (_)
        string description = incomingData switch
        {
            Circle c when c.Radius == 0 => "Точка",
            Circle { Radius: > 10 } => "Велике коло",
            Rectangle r => $"Прямокутник {r.Width}x{r.Height}",
            null => "Дані відсутні",
            _ => "Невідома фігура" // Обробка всього іншого
        };
    }

    static object GetShapeFromNetwork()
    {
        return new Circle
        {
            Radius = 15
        };
    }
}

class Circle
{
    public double Radius { get; set; }
}

class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
}