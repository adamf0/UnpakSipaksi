using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRumpunIlmu;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateRumpunIlmu
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/RumpunIlmu", async (UpdateRumpunIlmuRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRumpunIlmuCommand(
                    request.UuidPenelitianHibah,
                    request.UuidRumpunIlmu1,
                    request.UuidRumpunIlmu2,
                    request.UuidRumpunIlmu3
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateRumpunIlmuRequest
        {
            public string UuidPenelitianHibah { get; set; }
            public string? UuidRumpunIlmu1 { get; set; }
            public string? UuidRumpunIlmu2 { get; set; }
            public string? UuidRumpunIlmu3 { get; set; }
        }
    }
}
