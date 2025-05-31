using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Common.Presentation.FileManager;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateDokumenPendukung
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/DokumenPendukung", [IgnoreAntiforgeryToken(Order = 1001)]  async ([FromForm] UpdateDokumenPendukungRequest request, ISender sender, IFileProvider fileProvider, HttpContext context) =>
            {
                string? dokumenPath = null;
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads/penelitian_internal/dokumen_pendukung");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (request.File != null && request.File.Length > 0)
                {
                    string safeFileName = fileProvider.GenerateFileName(request.File);
                    string extension = fileProvider.GetSafeExtension(request.File);

                    var filePath = Path.Combine(uploadsFolder, safeFileName);

                    // Optional file size and extension validation
                    if (request.File.Length > 5 * 1024 * 1024) // 5 MB limit
                    {
                        return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "File size is too large.")));
                    }

                    var allowedExtensions = new[] { "pdf"};
                    if (!allowedExtensions.Contains(extension.ToLower()))
                    {
                        return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "Invalid file type")));
                    }
                    if (!fileProvider.IsSafeMimeType(request.File))
                    {
                        return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "Invalid mime type")));
                    }
                    if (!fileProvider.IsValidMimeTypeAllowedExtension(request.File.ContentType, extension.ToLower()))
                    {
                        return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "Invalid allowed extension in mime type")));
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(stream);
                    }

                    dokumenPath = "penelitian_internal/dokumen_pendukung/" + safeFileName; // Relative path
                }

                Result result = await sender.Send(new UpdateDokumenPendukungCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    dokumenPath,
                    request.Link,
                    request.Kategori
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah)
                .Accepts<UpdateDokumenPendukungRequest>("multipart/form-data")
                .DisableAntiforgery();
        }

        internal sealed class UpdateDokumenPendukungRequest
        {
            [FromForm] public string Uuid { get; set; }
            [FromForm] public string UuidPenelitianHibah { get; set; }
            [FromForm] public IFormFile? File { get; set; }
            [FromForm] public string? Link { get; set; }
            [FromForm] public string Kategori { get; set; }
        }
    }
}
