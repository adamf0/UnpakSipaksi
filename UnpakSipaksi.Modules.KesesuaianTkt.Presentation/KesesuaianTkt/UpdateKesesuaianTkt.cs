using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.UpdateKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt
{
    internal static class UpdateKesesuaianTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KesesuaianTkt", async (UpdateKesesuaianTktRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKesesuaianTktCommand(
                    request.Id,
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianTkt);
        }

        internal sealed class UpdateKesesuaianTktRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
