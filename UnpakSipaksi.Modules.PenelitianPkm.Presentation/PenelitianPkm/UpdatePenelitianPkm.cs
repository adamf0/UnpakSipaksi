using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdatePenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdatePenelitianPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm", async (UpdatePenelitianPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePenelitianPkmCommand(
                    request.Id,
                    request.NIDN,
                    request.TahunPengajuan,
                    request.Judul
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdatePenelitianPkmRequest
        {
            public string Id { get; set; }
            public string NIDN { get; set; }
            public string TahunPengajuan { get; set; }
            public string Judul { get; set; }
        }
    }
}
