namespace BaseDotNetApp.Lecture10;

public static class Deconstruction
{
    public static void Exec()
    {
        LogisticsNode lNode = new LogisticsNode();
        lNode.HubId = "1111-2222-3333";
        lNode.Coords = new GeoCoordinates(10.0, 20.0);
        ProcessNode(lNode);
    }
    
    public static void ProcessNode(LogisticsNode node)
    {
        var (hubId, lat, lon) = node;
        var point = new Point();
        var (x, y) = point.getTuple();

        // // Використання деконструкції (Positional pattern) безпосередньо у шаблоні
        // if (node is var (hubId, lat, lon) && lat > 0)
        // // За допомогою патерна
        // // if (node is (var hubId, > 0, var lon))
        // {
        //     Console.WriteLine($"Хаб {hubId} розташований у Північній півкулі. Довгота: {lon}");
        // }
    }

}

public record GeoCoordinates(double Latitude, double Longitude);

public class LogisticsNode 
{
    public string HubId { get; set; }
    public GeoCoordinates Coords { get; set; }

    // Кастомний метод Deconstruct для класичного класу
    public void Deconstruct(out string hubId, out double lat, out double lon)
    {
        hubId = HubId;
        lat = Coords.Latitude;
        lon = Coords.Longitude;
    }
}

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    
    public (double, double) getTuple() => (X, Y);
}

public class Result
{
    public string Message { get; set; }
    public string Status { get; set; }
}