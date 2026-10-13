namespace BaseDotNetApp.Lecture8;

public static class LiveExample
{
    public static void Exec()
    {
        Dalmatian dalmatian = new Dalmatian("dalmatian1", 5);
        Dog dog1 = new Dog("dog1", 10);
        object obj = dalmatian;
        foo(new Cat("cat1", 10));
        
    }
    

    private static void foo(Animal obj)
    {
        switch (obj)
        {
            case Cat cat when cat.Age == 15:    
                Console.WriteLine("ювілей");
                Console.WriteLine("this is cat");
                break;
            case Dalmatian dalmatian:
                Console.WriteLine("this is dalmatian");
                break;
            default:
                break;
        }
    }
}

public abstract class Animal(string name, int age)
{
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public virtual void say()
    {
        Console.WriteLine("Animal said something");
    }
}

public class Dog(string name, int age) : Animal(name, age)
{

    public sealed override void say()
    {
        Console.WriteLine("Dog said something");
    }
}

public class Dalmatian(string name, int age) : Dog(name, age)
{
    public new void say()
    {
        Console.WriteLine("Dalmatian said something");
    }
}

public class Cat(string name, int age) : Animal(name, age)
{
    public override void say()
    {
        Console.WriteLine("Cat said something");
    }
}