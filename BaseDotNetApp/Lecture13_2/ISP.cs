namespace BaseDotNetApp.Lecture13_2;

public class ISP
{
    
}
// Вузькоспеціалізовані інтерфейси
public interface IFileUploader
{
    void UploadFile(string name, byte[] data);
}

public interface IFileDownloader
{
    byte[] DownloadFile(string name);
    string GenerateTemporaryDownloadLink(string name, TimeSpan expiry);
}

public interface IAccessManager
{
    void ManageAccessControlList(string name, string permissions);
}

// Конкретний клас-адаптер реалізує всі необхідні ролі
public class AwsS3Storage : IFileUploader, IFileDownloader, IAccessManager
{
    public void UploadFile(string name, byte[] data) { /* Взаємодія з AWS */ }
    public byte[] DownloadFile(string name) { return []; }
    public void ManageAccessControlList(string name, string permissions) { /* Налаштування IAM */ }
    public string GenerateTemporaryDownloadLink(string name, TimeSpan expiry) => "https://s3...";
}

// Тепер ReportGenerator залежить виключно від того контракту, який він реально використовує
public class ReportGenerator(IFileUploader uploader)
{
    public void GenerateAndSave(string reportName)
    {
        byte[] data = [1, 2, 3, 4, 5];
        uploader.UploadFile(reportName, data);
    }
}