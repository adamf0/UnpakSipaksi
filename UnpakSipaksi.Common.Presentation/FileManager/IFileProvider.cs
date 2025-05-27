using Microsoft.AspNetCore.Http;

namespace UnpakSipaksi.Common.Presentation.FileManager
{
    public interface IFileProvider
    {
        bool IsSafeFileExtension(string extension);
        string GenerateFileName(IFormFile file);
        string GetSafeExtension(IFormFile file);
        bool IsSafeMimeType(IFormFile file);
        bool IsValidMimeTypeAllowedExtension(string mimeType, string extension);
    }
}
