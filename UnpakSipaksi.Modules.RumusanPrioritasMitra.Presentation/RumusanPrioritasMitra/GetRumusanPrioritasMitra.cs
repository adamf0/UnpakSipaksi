using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra
{
    internal static class GetRumusanPrioritasMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RumusanPrioritasMitra/{id}", async (Guid id, ISender sender) =>
            {
                Result<RumusanPrioritasMitraResponse> result = await sender.Send(new GetRumusanPrioritasMitraQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumusanPrioritasMitra);
        }
    }
}
