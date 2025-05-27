using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSkema.Presentation;
using UnpakSipaksi.Modules.KategoriSkema.Application.UpdateKategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema
{
    internal static class UpdateKategoriSkema
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KategoriSkema", async (UpdateKategoriSkemaRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriSkemaCommand(
                    request.Id,
                    request.Nama,
                    request.Rule
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriSkema);
        }

        internal sealed class UpdateKategoriSkemaRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public string Rule { get; set; }
        }
    }
}
