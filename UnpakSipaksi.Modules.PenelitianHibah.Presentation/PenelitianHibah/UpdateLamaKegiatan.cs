using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLamaKegiatan;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdatePenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateLamaKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/LamaKegiatan", async (UpdateLamaKegiatanRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberDosenCommand(
                    request.Id,
                    request.LamaKegiatan
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateLamaKegiatanRequest
        {
            public string Id { get; set; }
            public int LamaKegiatan { get; set; }
        }
    }
}
