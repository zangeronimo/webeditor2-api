using Microsoft.AspNetCore.Http;
using Webeditor.Domain.Interfaces.Infra;

namespace Webeditor.Infra.Providers.FileUploadProvider;

public class BufferedFileUploadLocalProvider : IFileUploadProvider
{
  public bool DeleteFile(string path)
  {
    try
    {
      var pathname = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
      if (!File.Exists($"{pathname}/{path}"))
        return false;

      File.Delete($"{pathname}/{path}");
      return true;
    }
    catch
    {
      throw new ArgumentException("File delete failed!");
    }
  }

  public async Task<string> UploadFileAsync(string base64file, string userPath)
  {
    string path = "";
    try
    {
      path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles", userPath));
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      var name = Guid.NewGuid().ToString();
      using (var fileStream = new FileStream(Path.Combine(path, name), FileMode.Create))
      {
        byte[] bytes = Convert.FromBase64String(base64file);
        MemoryStream stream = new MemoryStream(bytes);

        IFormFile file = new FormFile(stream, 0, bytes.Length, name, name);
        await file.CopyToAsync(fileStream);
      }
      return $"/{userPath}/{name}";
    }
    catch (Exception ex)
    {
      throw new Exception("File copy failed!", ex);
    }
  }
}

