using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.CreateSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Presentation;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Presentation.SotaKebaharuan
{
    internal static class CreateSotaKebaharuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("SotaKebaharuan", async (CreateSotaKebaharuanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateSotaKebaharuanCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.SotaKebaharuan);
        }

        internal sealed class CreateSotaKebaharuanRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
