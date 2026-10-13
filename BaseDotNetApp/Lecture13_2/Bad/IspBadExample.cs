namespace BaseDotNetApp.Lecture13_2.Bad;

public class IspBadExample
{
    
}

// Антипатерн: Fat interface (Роздутий інтерфейс)
public interface ICloudStorage
{
    void UploadFile(string name, byte[] data);
    byte[] DownloadFile(string name);
    void ManageAccessControlList(string name, string permissions);
    string GenerateTemporaryDownloadLink(string name, TimeSpan expiry);
}

// Цей сервіс має потребу лише у завантаженні файлу,
// але отримує доступ до методів зміни прав безпеки.
public class ReportGenerator(ICloudStorage storage)
{
    public void GenerateAndSave(string reportName)
    {
        byte[] data = [1, 2, 3, 4, 5]; // Collection expression (C# 12)
        storage.UploadFile(reportName, data);
    }
}