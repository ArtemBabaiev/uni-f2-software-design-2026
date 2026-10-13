using System;

namespace BaseDotNetApp;

class Person
{
    private string address;

    public Person(string fullname, int age)
    {
        this.address = fullname + age;
    }
    
    public Person(string fullname, int age, string address): this(fullname, age)
    {
        this.address = address;
    }

    public string Name
    {
        get;
        init;
    }

    ~Person()
    {
        Console.WriteLine("In finalizer");
    }

    public override string ToString()
    {
        return $"Person{this.address}; {this.Name}";
    }

    public override bool Equals(object? obj)
    {
        Console.WriteLine("IN equals");
        if (obj is Person person)
        {
            return this.Name == person.Name && this.address == person.address;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Name, this.address);
    }
}