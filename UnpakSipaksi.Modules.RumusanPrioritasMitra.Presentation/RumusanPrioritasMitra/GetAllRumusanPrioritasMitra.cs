using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetAllRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra
{
    internal class GetAllRumusanPrioritasMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RumusanPrioritasMitra", async (ISender sender) =>
            {
                Result<List<RumusanPrioritasMitraResponse>> result = await sender.Send(new GetAllRumusanPrioritasMitraQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumusanPrioritasMitra);
        }
    }
}
