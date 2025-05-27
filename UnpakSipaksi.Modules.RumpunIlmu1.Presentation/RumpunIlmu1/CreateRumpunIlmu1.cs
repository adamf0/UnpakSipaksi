using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.CreateRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1
{
    internal static class CreateRumpunIlmu1
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RumpunIlmu1", async (CreateRumpunIlmu1Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRumpunIlmu1Command(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu1);
        }

        internal sealed class CreateRumpunIlmu1Request
        {
            public string Nama { get; set; }
        }
    }
}
