namespace BaseDotNetApp.Lecture14;


public class HighCoupling
{
    public class SqlServerRepository
    {
        public string FetchData(string reportId)
        {
            return "data";
        }
    }

    public class LocalFileStorage
    {
        public void SaveFile(string reportId, string report)
        {
            
        }
    }
    
    public class ReportGenerator
    {
        // Жорстка залежність від конкретних класів (High Coupling)
        private readonly SqlServerRepository _db = new();
        private readonly LocalFileStorage _storage = new();

        public void GenerateAndSave(string reportId)
        {
            var data = _db.FetchData(reportId);
            var formattedReport = $"Report: {data}";
            _storage.SaveFile(reportId, formattedReport);
        }
    }

}

public class LowCoupling
{
    public interface IDataRepository
    {
        string FetchData(string id);
    }

    public interface IStorage
    {
        void SaveFile(string id, string content);
    }

    // Використання Primary Constructor для впровадження залежностей (C# 12)
    public class ReportGenerator(IDataRepository db, IStorage storage)
    {
        private readonly IDataRepository _db = db;
        private readonly IStorage _storage = storage;

        public void GenerateAndSave(string reportId)
        {
            var data = _db.FetchData(reportId);
            var formattedReport = $"Report: {data}";
            _storage.SaveFile(reportId, formattedReport);
        }
    }
}