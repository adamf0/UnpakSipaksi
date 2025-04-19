using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.UpdateAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian
{
    internal static class UpdateAkurasiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("AkurasiPenelitian", async (UpdateAkurasiPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateAkurasiPenelitianCommand(
                    request.Id,
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.AkurasiPenelitian);
        }

        internal sealed class UpdateAkurasiPenelitianRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
