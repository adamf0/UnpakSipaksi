using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.UpdateKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian
{
    internal static class UpdateKategoriProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KategoriProgramPengabdian", async (UpdateKategoriProgramPengabdianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriProgramPengabdianCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.Rule
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriProgramPengabdian);
        }

        internal sealed class UpdateKategoriProgramPengabdianRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Rule { get; set; }
        }
    }
}
