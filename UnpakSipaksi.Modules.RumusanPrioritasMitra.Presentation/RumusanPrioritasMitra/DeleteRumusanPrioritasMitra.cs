using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.DeleteRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra
{
    internal class DeleteRumusanPrioritasMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RumusanPrioritasMitra/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRumusanPrioritasMitraCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RumusanPrioritasMitra);
        }
    }
}
