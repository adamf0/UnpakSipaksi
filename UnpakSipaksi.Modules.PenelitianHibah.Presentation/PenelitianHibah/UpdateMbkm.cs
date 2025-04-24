using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMbkm;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateMbkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/MbkmMemberMahasiswa", async (UpdateMbkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMbkmCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    request.NPM,
                    request.BuktiMbkm
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateMbkmRequest
        {
            public string Uuid { get; set; }
            public string NPM { get; set; }
            public string UuidPenelitianHibah { get; set; }
            public string BuktiMbkm { get; set; }
        }
    }
}
