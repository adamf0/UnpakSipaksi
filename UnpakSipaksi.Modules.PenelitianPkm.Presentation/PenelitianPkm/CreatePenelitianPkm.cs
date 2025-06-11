using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreatePenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreatePenelitianPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm", async (CreatePenelitianPkmRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePenelitianPkmCommand(
                    request.NIDN,
                    request.TahunPengajuan,
                    request.Judul
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class CreatePenelitianPkmRequest
        {
            public string NIDN { get; set; }
            public string TahunPengajuan { get; set; }
            public string Judul { get; set; }
        }
    }
}
