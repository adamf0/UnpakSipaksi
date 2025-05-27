using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriTkt.Application.CreateKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Presentation;

namespace UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt
{
    internal static class CreateKategoriTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KategoriTkt", async (CreateKategoriTktRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriTktCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriTkt);
        }

        internal sealed class CreateKategoriTktRequest
        {
            public string Nama { get; set; }
        }
    }
}
