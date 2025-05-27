using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Satuan.Application.CreateSatuan;
using UnpakSipaksi.Modules.Satuan.Presentation;

namespace UnpakSipaksi.Modules.Satuan.Presentation.Satuan
{
    internal static class CreateSatuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Satuan", async (CreateSatuanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateSatuanCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Satuan);
        }

        internal sealed class CreateSatuanRequest
        {
            public string Nama { get; set; }
        }
    }
}
