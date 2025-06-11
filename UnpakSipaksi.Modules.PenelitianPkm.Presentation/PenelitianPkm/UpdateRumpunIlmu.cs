using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateRumpunIlmu;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateRumpunIlmu
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/RumpunIlmu", async (UpdateRumpunIlmuPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRumpunIlmuCommand(
                    request.UuidPenelitianPkm,
                    request.UuidRumpunIlmu1,
                    request.UuidRumpunIlmu2,
                    request.UuidRumpunIlmu3
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateRumpunIlmuPkmRequest
        {
            public string UuidPenelitianPkm { get; set; }
            public string? UuidRumpunIlmu1 { get; set; }
            public string? UuidRumpunIlmu2 { get; set; }
            public string? UuidRumpunIlmu3 { get; set; }
        }
    }
}
