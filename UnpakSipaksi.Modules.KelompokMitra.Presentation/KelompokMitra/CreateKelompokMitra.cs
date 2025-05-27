using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokMitra.Presentation;
using UnpakSipaksi.Modules.KelompokMitra.Application.CreateKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra
{
    internal static class CreateKelompokMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KelompokMitra", async (CreateKelompokMitraRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKelompokMitraCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KelompokMitra);
        }

        internal sealed class CreateKelompokMitraRequest
        {
            public string Nama { get; set; }
        }
    }
}
