using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.CreateKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian
{
    internal static class CreateKategoriMitraPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KategoriMitraPenelitian", async (CreateKategoriMitraPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriMitraPenelitianCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriMitraPenelitian);
        }

        internal sealed class CreateKategoriMitraPenelitianRequest
        {
            public string Nama { get; set; }
        }
    }
}
