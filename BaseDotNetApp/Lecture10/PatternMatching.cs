namespace BaseDotNetApp.Lecture10;

public static class PatternMatching
{
    public static void Exec()
    {
        Console.WriteLine(AnalyzeSensorTelemetry(0));
    }
    
    public static string AnalyzeSensorTelemetry(object telemetryData)
    {
        if (telemetryData is SensorData sensorData)
        {
            bool res = sensorData is null;
        }
        return telemetryData switch
        {
            // Type pattern + Relational pattern + Logical 'or'/'and'
            int temp and (> 40 or < -10) => $"Критична температура: {temp}°C",
            int temp and (>= -10 and <= 40) => $"Нормальна температура: {temp}°C",

            // Property pattern - глибока перевірка внутрішнього стану без приведення
            SensorData { IsCalibrated: false, Model: "TX-900" } => "Потрібна калібровка TX-900",
            SensorData data => $"Дані валідного сенсора: {data.Model}",

            // Constant pattern для null-безпеки
            null => "Відсутні телеметричні дані",
            _ => "Невідомий формат пакету даних"
        };
    }
}

public class SensorData
{
    public bool IsCalibrated {get; set;}
    public string Model {get; set;}

    // public static bool operator ==(SensorData? left, SensorData? right)
    // {
    //     return left is null && right is null;
    // }
    
}
