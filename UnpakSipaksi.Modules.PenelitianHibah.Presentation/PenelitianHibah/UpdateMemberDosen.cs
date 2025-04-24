using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/MemberDosen", async (UpdateMemberDosenRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberDosenCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    request.NIDN
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateMemberDosenRequest
        {
            public string Uuid { get; set; }
            public string NIDN { get; set; }
            public string UuidPenelitianHibah { get; set; }
        }
    }
}
