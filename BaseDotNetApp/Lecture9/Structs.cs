namespace BaseDotNetApp.Lecture9;

public static class Structs
{
    public static void Exec()
    {
        Vector3D obj = new Vector3D(1.0, 2.5, 3.1);
        Process(ref obj);
        Console.WriteLine(obj.X);
    }

    public static void Process(ref Vector3D obj)
    {
        obj.X = 100;
    }
}

// readonly struct гарантує, що поля не зміняться після створення.
// Реалізація IEquatable<T> дозволяє уникати пакування (boxing) під час порівнянь.
public ref struct Vector3D : IEquatable<Vector3D>
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3D(double x, double y, double z) { X = x; Y = y; Z = z; }
    public bool Equals(Vector3D other) => X == other.X && Y == other.Y && Z == other.Z;
    //public override bool Equals(object? obj) => obj is Vector3D other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}



