using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.CreateRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra
{
    internal static class CreateRumusanPrioritasMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RumusanPrioritasMitra", async (CreateRumusanPrioritasMitraRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRumusanPrioritasMitraCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumusanPrioritasMitra);
        }

        internal sealed class CreateRumusanPrioritasMitraRequest
        {
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
