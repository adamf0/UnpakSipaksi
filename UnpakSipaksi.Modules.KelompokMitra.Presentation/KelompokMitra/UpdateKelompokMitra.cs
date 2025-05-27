using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokMitra.Presentation;
using UnpakSipaksi.Modules.KelompokMitra.Application.UpdateKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra
{
    internal static class UpdateKelompokMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KelompokMitra", async (UpdateKelompokMitraRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKelompokMitraCommand(
                    request.Id,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KelompokMitra);
        }

        internal sealed class UpdateKelompokMitraRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
