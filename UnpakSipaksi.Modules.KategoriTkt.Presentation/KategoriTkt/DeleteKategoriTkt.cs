using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriTkt.Application.DeleteKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Presentation;

namespace UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt
{
    internal class DeleteKategoriTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KategoriTkt/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriTktCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriTkt);
        }
    }
}
