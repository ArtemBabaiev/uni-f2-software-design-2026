namespace BaseDotNetApp.Lecture13_2;

public class LSP
{
    
}

// Розділення можливостей через сегрегацію інтерфейсів створює правильну ієрархію
public interface IDocument
{
    string Content { get; }
}

public interface IWritableDocument : IDocument
{
    void SetContent(string content);
}

// Реалізація з можливістю запису дотримується контракту IWritableDocument
public class WritableDocument : IWritableDocument
{
    public string Content { get; private set; }
    
    public WritableDocument(string initialContent)
    {
        SetContent(initialContent);
    }

    public void SetContent(string content)
    {
        // Сучасні Guard Clauses (C# 10+) замість System.Diagnostics.Contracts
        // Забезпечення передумови (Precondition)
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        
        // Збереження доменного інваріанта
        Content = content;
    }
}

// ReadOnlyDocument більше не порушує LSP, оскільки він просто реалізує IDocument,
// і не бере на себе зобов'язань щодо контракту IWritableDocument.
public class ReadOnlyDocument : IDocument
{
    public string Content { get; }

    // Валідація інваріанту на етапі створення через C# 12 Primary Constructor
    public ReadOnlyDocument(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        Content = content;
    }
}