using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Common.Presentation.FileManager;
using UnpakSipaksi.Common.Presentation.Security;
using UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman
{
    internal static class CreatePengumuman
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Pengumuman", [IgnoreAntiforgeryToken(Order = 1001)] async ([FromForm] CreatePengumumanRequest request, ISender sender, IFileProvider fileProvider, HttpContext context, TokenValidator tokenValidator) =>
            {
                string? dokumenPath = null;
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads/pengumuman");

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

                    var allowedExtensions = new[] { "pdf" };
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

                    dokumenPath = "pengumuman/" + safeFileName; // Relative path
                }

                Result<Guid> result = await sender.Send(new CreatePengumumanCommand(
                    request.Pesan,
                    dokumenPath,
                    request.Url,
                    request.Type,
                    request.Target,
                    request.Nidn,
                    request.KodeFaKultas,
                    request.TypeExpired,
                    request.TanggalAwal,
                    request.TanggalAkhir
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Pengumuman)
                .Accepts<CreatePengumumanRequest>("multipart/form-data")
                .DisableAntiforgery();
        }

        internal sealed class CreatePengumumanRequest
        {
            [FromForm] public string Pesan { get; private set; } = null!;
            [FromForm] public IFormFile? File { get; private set; }
            [FromForm] public string? Url { get; private set; }
            [FromForm] public string Type { get; private set; } = null!;
            [FromForm] public string? Target { get; private set; }
            [FromForm] public string? Nidn { get; private set; }
            [FromForm] public string? KodeFaKultas { get; private set; }
            [FromForm] public string TypeExpired { get; private set; }
            [FromForm] public string? TanggalAwal { get; private set; }
            [FromForm] public string? TanggalAkhir { get; private set; }
        }
    }
}
