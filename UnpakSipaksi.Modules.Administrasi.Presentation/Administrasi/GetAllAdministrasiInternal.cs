using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Application.GetAllAdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiInternal
{
    internal class GetAllAdministrasiInternal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AdministrasiInternal/{UuidPenelitianHibah}", async (Guid UuidPenelitianHibah, ISender sender) =>
            {
                Result<List<AdministrasiInternalResponse>> result = await sender.Send(new GetAllAdministrasiInternalQuery(UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }
    }
}
