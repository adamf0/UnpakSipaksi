using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.CreateKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt
{
    internal static class CreateKesesuaianTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KesesuaianTkt", async (CreateKesesuaianTktRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKesesuaianTktCommand(
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianTkt);
        }

        internal sealed class CreateKesesuaianTktRequest
        {
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
