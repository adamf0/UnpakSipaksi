using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMbkm;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateMbkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/MbkmMemberMahasiswa", async (UpdateMbkmPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMbkmCommand(
                    request.Uuid,
                    request.UuidPenelitianPkm,
                    request.NPM,
                    request.BuktiMbkm
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateMbkmPkmRequest
        {
            public string Uuid { get; set; }
            public string NPM { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string BuktiMbkm { get; set; }
        }
    }
}
