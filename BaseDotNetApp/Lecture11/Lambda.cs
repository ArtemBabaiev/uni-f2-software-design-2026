namespace BaseDotNetApp.Lecture11;

public class Lambda
{
    public static void Exec()
    {
        var print = (string param) =>
        {
            Console.WriteLine(param);
        };
        
        print("hello world");

        callCustomFunction(print);
    }

    private static void callCustomFunction(Action<string> print)
    {
        print.Invoke("hello world");
    }
}