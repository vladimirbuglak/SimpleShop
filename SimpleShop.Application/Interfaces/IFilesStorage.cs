namespace SimpleShop.Application.Modify.Interfaces;

public interface IFilesStorage
{
    Task<string> UploadAsync(UploadFile file);
}

public class UploadFile
{
    public string Name { get; set; }
    
    public byte[] Content { get; set; }
}