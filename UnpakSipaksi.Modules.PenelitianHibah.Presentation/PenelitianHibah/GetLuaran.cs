using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetLuaran;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/Luaran/{Uuid}/{UuidPenelitianHibah}", async (string Uuid, string UuidPenelitianHibah, ISender sender) =>
            {
                Result<LuaranResponse> result = await sender.Send(new GetLuaranQuery(Uuid,UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
