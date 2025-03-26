using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.UpdateRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra
{
    internal static class UpdateRumusanPrioritasMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RumusanPrioritasMitra", async (UpdateRumusanPrioritasMitraRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRumusanPrioritasMitraCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumusanPrioritasMitra);
        }

        internal sealed class UpdateRumusanPrioritasMitraRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
