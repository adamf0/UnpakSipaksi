using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.UpdateRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Presentation.RumpunIlmu3
{
    internal static class UpdateRumpunIlmu3
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RumpunIlmu3", async (UpdateRumpunIlmu3Request request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRumpunIlmu3Command(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.UuidRumpunIlmu2
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu3);
        }

        internal sealed class UpdateRumpunIlmu3Request
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public Guid UuidRumpunIlmu2 { get; set; }
        }
    }
}
