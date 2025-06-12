using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Common.Presentation.FileManager;
using UnpakSipaksi.Common.Presentation.Security;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreateDokumenMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm/DokumenMitra", [IgnoreAntiforgeryToken(Order = 1001)] async ([FromForm] CreateDokumenMitraRequest request, ISender sender, IFileProvider fileProvider, HttpContext context, TokenValidator tokenValidator) =>
            {
                string? dokumenPath = null;
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads/pkm/dokumen_mitra");

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

                    dokumenPath = "pkm/dokumen_mitra/" + safeFileName; // Relative path
                }

                Result<Guid> result = await sender.Send(new CreateDokumenMitraCommand(
                    request.UuidPenelitianPkm,
                    request.Mitra,
                    request.Provinsi,
                    request.Kota,
                    request.UuidKelompokMitra,
                    request.PemimpinMitra,
                    request.KontakMitra,
                    dokumenPath ?? ""
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm)
              .Accepts<CreateDokumenMitraRequest>("multipart/form-data")
              .DisableAntiforgery();
        }

        internal sealed class CreateDokumenMitraRequest
        {
            [FromForm] public string UuidPenelitianPkm { get; set; }
            [FromForm] public string Mitra { get; set; }
            [FromForm] public string Provinsi { get; set; }
            [FromForm] public string Kota { get; set; }
            [FromForm] public string UuidKelompokMitra { get; set; }
            [FromForm] public string PemimpinMitra { get; set; }
            [FromForm] public string? KontakMitra { get; set; }
            [FromForm] public IFormFile? File { get; set; }
        }
    }
}
