using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal class DeleteMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianPkm/MemberNonDosen/{uuid}", async (string uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteMemberNonDosenCommand(uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
