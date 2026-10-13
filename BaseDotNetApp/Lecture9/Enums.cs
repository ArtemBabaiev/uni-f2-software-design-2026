namespace BaseDotNetApp.Lecture9;

public static class Enums
{
    public static void Exec()
    {
        TransactionState.Canceled.IsTerminal();
        
        bool isDefined = Enum.IsDefined(typeof(TransactionState), 6);
        if (isDefined)
        {
     
        }
        TransactionState state = (TransactionState) 6;

        Console.WriteLine(state.ToString());

        WeekdayFlagged monday = WeekdayFlagged.Monday;
        monday.GetLabel();

        WeekdayFlagged weekdayFlagged = WeekdayFlagged.Monday | WeekdayFlagged.Friday;
        // 0b_00000001
        // 0b_00010000
        //
        // 0b_00010001
        // 0b_00000010
        // 0b_00000000
        bool isMonday = (weekdayFlagged & WeekdayFlagged.Monday) != WeekdayFlagged.None;
        var hasFlag = weekdayFlagged.HasFlag(WeekdayFlagged.Monday);
    }
}

// Обмеження типу до byte для економії пам'яті
public enum TransactionState : byte
{
    Created = 1, 
    Processing = 2,
    Finished = 3,
    Canceled = 4,
    Error = 5
}

public static class TransactionStateExtension
{
    public static bool IsTerminal(this TransactionState state)
    {
        return state is TransactionState.Finished or TransactionState.Canceled or TransactionState.Error;
    }
}

// Бітові маски (комбінації станів)
[Flags]
public enum WeekdayFlagged
{
    None       = 0,
    Monday     = 1 << 0, // 0b_00000001 // 1
    Tuesday    = 1 << 1, // 0b_00000010 // 2
    Wednesday  = 1 << 2, // 0b_00000100 // 4
    Thursday   = 1 << 3, // 0b_00001000 // 8
    Friday     = 1 << 4, // 0b_00010000 // 16
    Saturday   = 1 << 5, // 0b_00100000 // 32
    Sunday     = 1 << 6, // 0b_01000000 // 64

    Weekends   = Saturday | Sunday, // 0b_01100000
    Business   = Monday | Tuesday | Wednesday | Thursday | Friday // 0b_00011111
}

public static class WeekdayFlaggedExtension
{
    public static string GetLabel(this WeekdayFlagged state)
    {
        return state switch
        {
            WeekdayFlagged.Business => "Business"
        };
    }
}

