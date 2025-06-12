using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateProgramPengabdian;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/ProgramPengabdian", async (UpdateProgramPengabdianPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateProgramPengabdianCommand(
                  request.Uuid,
                  request.UuidPenelitianPkm,
                  request.UuidFokusPenelitian,
                  request.UuidRirn
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateProgramPengabdianPkmRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string? UuidFokusPenelitian { get; set; }
            public string? UuidRirn { get; set; }
        }
    }
}
