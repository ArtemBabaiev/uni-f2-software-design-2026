namespace BaseDotNetApp.Lecture10;

public static class SwitchExpressions
{
    public static void Exec()
    {
        CalculateServiceFee(CustomerLevel.Standard);
        CalculateServiceFee(CustomerLevel.Premium);
    }
    
    public static decimal CalculateServiceFee(CustomerLevel level)
    {
        return level switch
        {
            CustomerLevel.New => 15.00m,
            CustomerLevel.Standard => 10.50m,
            CustomerLevel.Premium => 5.00m,
            CustomerLevel.VIP => 0.00m,
            _ => throw new ArgumentOutOfRangeException(nameof(level), "Невідомий рівень")
        };
    }
}

public enum CustomerLevel
{
    New = 1,
    Standard = 2,
    Premium = 3,
    VIP = 4
}