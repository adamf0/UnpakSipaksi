using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokRab.Application.UpdateKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Presentation;

namespace UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab
{
    internal static class UpdateKelompokRab
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KelompokRab", async (UpdateKelompokRabRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKelompokRabCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KelompokRab);
        }

        internal sealed class UpdateKelompokRabRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
