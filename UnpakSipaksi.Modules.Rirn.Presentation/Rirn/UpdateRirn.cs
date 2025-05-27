using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Rirn.Presentation;
using UnpakSipaksi.Modules.Rirn.Application.UpdateRirn;

namespace UnpakSipaksi.Modules.Rirn.Presentation.Rirn
{
    internal static class UpdateRirn
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Rirn", async (UpdateRirnRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRirnCommand(
                    request.Id,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Rirn);
        }

        internal sealed class UpdateRirnRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
