using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.CreateKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian
{
    internal static class CreateKategoriProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KategoriProgramPengabdian", async (CreateKategoriProgramPengabdianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriProgramPengabdianCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.Rule
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriProgramPengabdian);
        }

        internal sealed class CreateKategoriProgramPengabdianRequest
        {
            public string Nama { get; set; }
            public string Rule { get; set; }
        }
    }
}
