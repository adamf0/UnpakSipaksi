using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokRab.Application.CreateKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Presentation;

namespace UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab
{
    internal static class CreateKelompokRab
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KelompokRab", async (CreateKelompokRabRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKelompokRabCommand(
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KelompokRab);
        }

        internal sealed class CreateKelompokRabRequest
        {
            public string Nama { get; set; }
        }
    }
}
