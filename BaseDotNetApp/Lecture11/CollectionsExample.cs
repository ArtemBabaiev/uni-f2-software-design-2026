namespace BaseDotNetApp.Lecture11;

public static class CollectionsExample
{
    public static void Exec()
    {
        List<int> myList = new List<int>(100);
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        var convertAll = myList.ConvertAll(input => input.ToString());
        myList.ForEach(Console.WriteLine);
        foreach (var i in myList)
        {
            
        }
        var readOnlyCollection = myList.AsReadOnly();
        var my = myList[1];
        myList[1] = 5;
        
        
        Dictionary<String, int> myDictionary = new Dictionary<string, int>();
        myDictionary.Add("one", 1);
        myDictionary.Add("two", 2);
        myDictionary.Add("one", 11);
        
        myDictionary["one"] = 1;
        myDictionary["two"] = 2;
        
        myDictionary.TryGetValue("three", out int myThree);
        Console.WriteLine(myThree);
    }
}