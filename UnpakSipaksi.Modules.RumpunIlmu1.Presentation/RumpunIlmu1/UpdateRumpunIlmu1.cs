using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.UpdateRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1
{
    internal static class UpdateRumpunIlmu1
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RumpunIlmu1", async (UpdateRumpunIlmu1Request request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRumpunIlmu1Command(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu1);
        }

        internal sealed class UpdateRumpunIlmu1Request
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public Guid TemaPenelitianId { get; set; }
        }
    }
}
