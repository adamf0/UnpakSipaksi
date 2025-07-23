using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Common.Domain;
using System;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;
using UnpakSipaksi.Modules.Insentif.Application.CalculateSbuInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;

namespace UnpakSipaksi.Modules.Insentif.Presentation.Insentif
{
    internal static class CalculateSbuInsentif
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Insentif/Calculate", async (CalculateSbuInsentifRequest request, ISender sender) =>
            {
                Result<SbuInsentifResponse> result = await sender.Send(new CalculateSbuInsentifCommand(
                    request.StatusJurnal,
                    request.PeranPenulis,
                    request.JumlahPenulis,
                    request.JenisJurnal,
                    request.LibatkanMahasiswa,
                    request.StatusPengajuan
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Insentif);
        }

        internal sealed class CalculateSbuInsentifRequest
        {
            public string StatusJurnal { get; set; }
            public string PeranPenulis { get; set; }
            public int JumlahPenulis { get; set; }
            public string JenisJurnal { get; set; }
            public string LibatkanMahasiswa { get; set; }
            public string StatusPengajuan { get; set; }
        }
    }
}
