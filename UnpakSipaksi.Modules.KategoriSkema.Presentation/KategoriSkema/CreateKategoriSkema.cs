using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSkema.Presentation;
using UnpakSipaksi.Modules.KategoriSkema.Application.CreateKategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema
{
    internal static class CreateKategoriSkema
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KategoriSkema", async (CreateKategoriSkemaRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriSkemaCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.Rule
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriSkema);
        }

        internal sealed class CreateKategoriSkemaRequest
        {
            public string Nama { get; set; }
            public string Rule { get; set; }
        }
    }
}
