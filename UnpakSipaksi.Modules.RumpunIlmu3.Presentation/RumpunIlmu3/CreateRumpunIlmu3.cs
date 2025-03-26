using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.CreateRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Presentation.RumpunIlmu3
{
    internal static class CreateRumpunIlmu3
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RumpunIlmu3", async (CreateRumpunIlmu3Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRumpunIlmu3Command(
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.UuidRumpunIlmu2
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu3);
        }

        internal sealed class CreateRumpunIlmu3Request
        {
            public string Nama { get; set; }
            public Guid UuidRumpunIlmu2 { get; set; }
        }
    }
}
