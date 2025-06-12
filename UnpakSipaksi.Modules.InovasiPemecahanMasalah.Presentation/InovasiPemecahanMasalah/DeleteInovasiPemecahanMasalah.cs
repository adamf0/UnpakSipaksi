using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.DeleteInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Presentation.InovasiPemecahanMasalah
{
    internal class DeleteInovasiPemecahanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("InovasiPemecahanMasalah/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteInovasiPemecahanMasalahCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.InovasiPemecahanMasalah);
        }
    }
}
