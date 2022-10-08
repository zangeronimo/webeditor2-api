namespace Webeditor.Domain.Interfaces.Infra;

public interface IFileUploadProvider
{
  Task<string> UploadFileAsync(string base64file, string userPath);

  bool DeleteFile(string path);
}
