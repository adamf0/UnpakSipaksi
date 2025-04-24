using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreatePenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class CreatePenelitianHibah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianHibah", async (CreatePenelitianHibahRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePenelitianHibahCommand(
                    request.NIDN,
                    request.TahunPengajuan,
                    request.Judul
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class CreatePenelitianHibahRequest
        {
            public string NIDN { get; set; }
            public string TahunPengajuan { get; set; }
            public string Judul { get; set; }
        }
    }
}
