using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.CreateReviewInsentif;
using System;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Presentation.Insentif
{
    internal static class CreateReviewInsentif
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Insentif/Review", async (CreateReviewInsentifRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new CreateReviewInsentifCommand(
                    request.Uuid,
                    request.BuktiPublikasi,
                    request.StatusJurnal,
                    request.PeranPenulis,
                    request.JumlahPenulis,
                    request.JenisJurnal,
                    request.LibatkanMahasiswa,
                    request.StatusPengajuan,
                    request.Catatan,
                    request.Type
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Insentif);
        }

        internal sealed class CreateReviewInsentifRequest
        {
            public string Uuid { get; set; }
            public string BuktiPublikasi { get; set; }
            public string StatusJurnal { get; set; }
            public string PeranPenulis { get; set; }
            public int JumlahPenulis { get; set; }
            public string JenisJurnal { get; set; }
            public string LibatkanMahasiswa { get; set; }
            public string StatusPengajuan { get; set; }
            public string? Catatan { get; set; }
            public string Type { get; set; }
        }
    }
}
