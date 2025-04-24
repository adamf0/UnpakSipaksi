using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/MemberNonDosen", async (UpdateMemberNonDosenRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberNonDosenCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    request.NomorIdentitas,
                    request.Nama,
                    request.Afiliasi
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateMemberNonDosenRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianHibah { get; set; }
            public string? NomorIdentitas { get; set; }
            public string? Nama { get; set; }
            public string? Afiliasi { get; set; }
        }
    }
}
