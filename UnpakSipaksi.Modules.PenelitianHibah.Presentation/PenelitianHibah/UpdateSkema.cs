using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLamaKegiatan;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdatePenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSkema;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateSkema
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/Skema", async (UpdateSkemaRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateSkemaCommand(
                    request.Id,
                    request.SkemaId,
                    request.TKT,
                    request.KategoriTKT
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateSkemaRequest
        {
            public string Id { get; set; }
            public string SkemaId { get; set; }
            public int TKT { get; set; }
            public string KategoriTKT { get; set; }
        }
    }
}
