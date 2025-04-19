using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;
using UnpakSipaksi.Modules.SubstansiBobot.Application.GetSubstansiBobot;

namespace UnpakSipaksi.Modules.SubstansiBobot.Presentation.SubstansiBobot
{
    internal class GetSubstansiBobot
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("SubstansiBobot/{Type}/{KategoriSkema}", async (string Type, string KategoriSkema, ISender sender) =>
            {
                Type = Type.ToLower().Trim();

                if (Type == "pkm")
                {
                    Result<SubstansiBobotPKMResponse> result = await sender.Send(new GetSubstansiBobotPKMQuery());
                    return result.Match(Results.Ok, ApiResults.Problem);
                }
                else if (Type == "hibah" && !KategoriSkema.IsNullOrEmpty())
                {
                    string[] validKategoriSkema =
                    [
                        "penelitian dasar",
                        "penelitian dosen Pemula (pdp)",
                        "penelitian terapan",
                        "penelitian kolaborasi"
                    ];


                    //mencegah broken rule
                    if (!validKategoriSkema.Contains(KategoriSkema.ToLower()))
                    {
                        return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "Unknown kategori skema")));
                    }

                    Result<SubstansiBobotHibahResponse> result = await sender.Send(new GetSubstansiBobotHibahQuery(KategoriSkema));
                    return result.Match(Results.Ok, ApiResults.Problem);
                }
                else
                {
                    return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "Unknown type")));
                }
            }).WithTags(Tags.SubstansiBobot);
        }
    }
}
