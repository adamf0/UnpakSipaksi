using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Presentation.RumpunIlmu2
{
    internal static class UpdateRumpunIlmu2
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RumpunIlmu2", async (UpdateRumpunIlmu2Request request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRumpunIlmu2Command(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.UuidRumpunIlmu1
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu2);
        }

        internal sealed class UpdateRumpunIlmu2Request
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public Guid UuidRumpunIlmu1 { get; set; }
        }
    }
}
