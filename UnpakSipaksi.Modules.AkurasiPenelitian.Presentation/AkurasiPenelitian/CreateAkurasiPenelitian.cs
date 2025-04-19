using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.CreateAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian
{
    internal static class CreateAkurasiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("AkurasiPenelitian", async (CreateAkurasiPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateAkurasiPenelitianCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.AkurasiPenelitian);
        }

        internal sealed class CreateAkurasiPenelitianRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
