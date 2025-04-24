using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdatePenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdatePenelitianHibah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah", async (UpdatePenelitianHibahRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePenelitianHibahCommand(
                    request.Id,
                    request.NIDN,
                    request.TahunPengajuan,
                    request.Judul
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdatePenelitianHibahRequest
        {
            public string Id { get; set; }
            public string NIDN { get; set; }
            public string TahunPengajuan { get; set; }
            public string Judul { get; set; }
        }
    }
}
