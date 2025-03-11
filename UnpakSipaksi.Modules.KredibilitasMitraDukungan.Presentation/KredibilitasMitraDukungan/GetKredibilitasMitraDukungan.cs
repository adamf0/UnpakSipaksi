using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation.KredibilitasMitraDukungan
{
    internal static class GetKredibilitasMitraDukungan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KredibilitasMitraDukungan/{id}", async (Guid id, ISender sender) =>
            {
                Result<KredibilitasMitraDukunganResponse> result = await sender.Send(new GetKredibilitasMitraDukunganQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KredibilitasMitraDukungan);
        }
    }
}
