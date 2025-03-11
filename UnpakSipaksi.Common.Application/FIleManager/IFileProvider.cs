using Microsoft.AspNetCore.Http;

namespace UnpakSipaksi.Common.Application.FIleManager
{
    public interface IFileProvider
    {
        bool IsSafeFileExtension(string extension);
        string GenerateFileName(IFormFile file);
        string GetSafeExtension(IFormFile file);
    }
}
