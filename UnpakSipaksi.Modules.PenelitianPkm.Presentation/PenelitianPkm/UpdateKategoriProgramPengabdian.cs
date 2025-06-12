using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateKategoriPengabdian;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateKategoriProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/KategoriPengabdian", async (UpdateKategoriProgramPengabdianPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriPengabdianCommand(
                  request.Uuid,
                  request.UuidPenelitianPkm,
                  request.UuidKategoriKategoriProgramPengabdian
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateKategoriProgramPengabdianPkmRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string UuidKategoriKategoriProgramPengabdian { get; set; }
        }
    }
}
