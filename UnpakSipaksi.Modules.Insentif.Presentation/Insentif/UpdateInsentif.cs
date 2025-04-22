using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.UpdateInsentifJurnal;
using UnpakSipaksi.Modules.Insentif.Application.UpdateInsentifProsiding;

namespace UnpakSipaksi.Modules.Insentif.Presentation.Insentif
{
    internal static class UpdateInsentif
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Insentif", async (UpdateInsentifRequest request, ISender sender) =>
            {
                if (request.JenisPublikasi.ToLower() == Constant.jurnalKey)
                {
                    // Handle external type insentif (Jurnal)
                    Result result = await sender.Send(new UpdateInsentifJurnalCommand(
                        request.Id,
                        request.Nidn,
                        request.JudulArtikel,
                        request.NamaJurnalPenerbit,
                        request.JumlahPenulis,
                        request.IndexJenisPublikasi,
                        request.Link,
                        ValueConverter.ConvertToInt(
                            Constant.filterJenisJurnal,
                            request.JenisJurnal.ToLower(),
                            -1
                        ), // 1: internal, 0: external
                        request.Peran.ToLower(), // First Author, Co Author
                        request.TanggalTerbit,
                        request.Volume,
                        request.Edisi,
                        request.Halaman,
                        request.DOI,
                        ValueConverter.ConvertToInt(
                            Constant.filterLibatkanMahasiswa,
                            request.LibatkanMahasiswa.ToLower(),
                            -1
                        ) // 1: ya, 0: tidak
                    ));

                    return result.Match(() => Results.Ok(), ApiResults.Problem);
                }
                else if (request.JenisPublikasi.ToLower() == Constant.prosidingKey)
                {
                    // Handle internal type insentif (Prosiding)
                    Result result = await sender.Send(new UpdateInsentifProsidingCommand(
                        request.Id,
                        request.Nidn,
                        request.JudulArtikel,
                        request.NamaJurnalPenerbit,
                        request.JumlahPenulis,
                        request.IndexJenisPublikasi,
                        request.Link,
                        ValueConverter.ConvertToInt(
                            Constant.filterJenisJurnal,
                            request.JenisJurnal,
                            -1
                        ), // 1: internal, 0: external
                        request.Peran.ToLower(), // First Author, Co Author
                        request.TanggalTerbit,
                        request.NamaKegiatanSeminar ?? "",
                        ValueConverter.ConvertToInt(
                            Constant.filterLibatkanMahasiswa,
                            request.LibatkanMahasiswa,
                            -1
                        ) // 1: ya, 0: tidak
                    ));

                    return result.Match(() => Results.Ok(), ApiResults.Problem);
                }
                else
                {
                    return ApiResults.Problem(Result.Failure(Error.Problem("Request.Invalid", "Tidak dapat eksekusi request")));
                }
            }).WithTags(Tags.Insentif);
        }

        internal sealed class UpdateInsentifRequest
        {
            public Guid Id { get; set; }
            public string Nidn { get; set; }
            public string JudulArtikel { get; set; }
            public string NamaJurnalPenerbit { get; set; }
            public int JumlahPenulis { get; set; }
            public string IndexJenisPublikasi { get; set; } //Guid
            public string Link { get; set; }
            public string JenisJurnal { get; set; } //1: internal 0:external
            public string Peran { get; set; } //First Author, Co Author
            public string TanggalTerbit { get; set; }
            public string JenisPublikasi { get; set; } //jurnal, prosiding
            public string? Volume { get; set; }
            public string? Edisi { get; set; }
            public string? Halaman { get; set; }
            public string? DOI { get; set; }
            public string? NamaKegiatanSeminar { get; set; }
            public string LibatkanMahasiswa { get; set; } // ya, tidak
        }
    }
}
