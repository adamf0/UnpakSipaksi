using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
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
                    request.UuidPenelitianHibah,
                    request.UuidSkema,
                    request.TKT,
                    request.UuidKategoriTKT
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateSkemaRequest
        {
            public string UuidPenelitianHibah { get; set; }
            public string UuidSkema { get; set; }
            public int TKT { get; set; }
            public string UuidKategoriTKT { get; set; }
        }
    }
}
