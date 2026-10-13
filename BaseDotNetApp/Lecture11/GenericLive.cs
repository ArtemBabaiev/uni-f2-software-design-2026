namespace BaseDotNetApp.Lecture11;

public class GenericLive
{
    public static void Exec()
    {
        List<string> items = new List<string>();
        items.Add("hello");
        items.Add("world");

        IEnumerable<string> enumerable = items.Where(i => i.Equals("hello"));


        List<string> filtered = new List<string>();
        foreach (var item in items)
        {
            if (item.Equals("hello"))
            {
                filtered.Add(item);
            }
        }
    }
    
}