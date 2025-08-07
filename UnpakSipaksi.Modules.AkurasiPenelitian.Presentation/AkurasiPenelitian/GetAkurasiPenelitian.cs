using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian
{
    //[PR] semua query harus divalidasi layaknya command agar mencegah erorr yg tidak terdeteksi
    internal static class GetAkurasiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AkurasiPenelitian/{id}", async (string id, ISender sender) =>
            {
                Result<AkurasiPenelitianResponse> result = await sender.Send(new GetAkurasiPenelitianQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.AkurasiPenelitian);
        }
    }
}
