namespace BaseDotNetApp.Lecture13_2.Bad;

public class LspBadExample
{
    
}

public class Document
{
    public string Content { get; protected set; } = string.Empty;

    public virtual void SetContent(string content)
    {
        // Передумова: контент не може бути null
        ArgumentNullException.ThrowIfNull(content);
        Content = content;
    }
}

public class ReadOnlyDocument : Document
{
    public override void SetContent(string content)
    {
        // Порушення LSP та контракту: Нащадок вводить нову несподівану поведінку.
        // Клієнт, працюючи зі списком List<Document>, не очікує NotSupportedException.
        throw new NotSupportedException("Cannot modify a read-only document.");
    }
}