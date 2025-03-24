using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Presentation.RumpunIlmu2
{
    internal static class CreateRumpunIlmu2
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RumpunIlmu2", async (CreateRumpunIlmu2Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRumpunIlmu2Command(
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.UuidRumpunIlmu1
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu2);
        }

        internal sealed class CreateRumpunIlmu2Request
        {
            public string Nama { get; set; }
            public Guid UuidRumpunIlmu1 { get; set; }
        }
    }
}
