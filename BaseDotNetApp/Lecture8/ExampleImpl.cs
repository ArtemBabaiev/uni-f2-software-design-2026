namespace BaseDotNetApp.Lecture8;

public class ExampleImpl : Example
{
    private string title;
    public string Title
    {
        get
        {
            if (title == null)
            {
                throw new InvalidDataException("null");
            }

            return title;
        }
        set => title = value;
    }
}