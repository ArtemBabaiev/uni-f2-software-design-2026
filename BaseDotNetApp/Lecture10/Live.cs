namespace BaseDotNetApp.Lecture10;

public class Live
{

    public string ToLower(string input) => input.ToLower();
    
    public static void Exec()
    {
        int[] array = new[] { 1, 2, 3 };
        if (array is [1,2,3])
        {
        }
    }
}