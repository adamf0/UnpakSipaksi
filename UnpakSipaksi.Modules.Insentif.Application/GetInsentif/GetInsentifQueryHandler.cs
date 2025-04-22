using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Application.GetInsentif
{
    internal sealed class GetInsentifQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetInsentifQuery, InsentifResponse>
    {
        public async Task<Result<InsentifResponse>> Handle(GetInsentifQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(i.uuid, '') AS VARCHAR(36)) AS Uuid,
                     i.NIDN AS Nidn,
                     i.judul_artikel AS JudulArtikel,
                     i.nama_jurnal_penerbit AS NamaJurnalPenerbit,
                     i.jumlah_penulis AS JumlahPenulis,
                     CAST(NULLIF(jp.Uuid, '') AS VARCHAR(36)) AS IndexJenisPublikasi,
                     i.link AS Link,
                     CASE 
                         WHEN i.jenis_jurnal = 0 THEN 'external'
                         WHEN i.jenis_jurnal = 1 THEN 'internal'
                         ELSE 'unknown'
                     END AS JenisJurnal,
                     LOWER(i.peran) AS Peran,
                     IFNULL(DATE_FORMAT(i.tanggal_terbit, '%Y-%m-%d'), '') AS TanggalTerbit,
                     i.volume AS Volume,
                     i.edisi AS Edisi,
                     i.halaman AS Halaman,
                     i.DOI AS DOI,
                     i.nama_kegiatan_seminar AS NamaKegiatanSeminar,
                     CASE 
                         WHEN i.mahasiswa = 0 THEN 'tidak'
                         WHEN i.mahasiswa = 1 THEN 'ya'
                         ELSE 'unknown'
                     END AS LibatkanMahasiswa,
                     CASE 
                         WHEN i.cek_insentif IS NOT NULL AND i.cek_insentif_v IS NULL THEN i.cek_insentif
                         WHEN (i.cek_insentif IS NULL AND i.cek_insentif_v IS NOT NULL) 
                              OR (i.cek_insentif IS NOT NULL AND i.cek_insentif_v IS NOT NULL) THEN i.cek_insentif_v
                         ELSE -1
                     END AS Insentif,
                     CASE 
                         WHEN i.status_pengajuan IS NOT NULL AND i.status_pengajuan_v IS NULL THEN i.status_pengajuan
                         WHEN (i.status_pengajuan IS NULL AND i.status_pengajuan_v IS NOT NULL) 
                              OR (i.status_pengajuan IS NOT NULL AND i.status_pengajuan_v IS NOT NULL) THEN i.status_pengajuan_v
                         ELSE 'unknown'
                     END AS Porsi,
                     i.porsi_sbu
                 FROM insentif i 
                 LEFT JOIN jenis_publikasi jp ON i.id_jenis_publikasi = jp.id
                 WHERE i.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<InsentifResponse?>(sql, new { Uuid = request.InsentifUuid });
            if (result == null)
            {
                return Result.Failure<InsentifResponse>(InsentifErrors.NotFound(request.InsentifUuid));
            }

            return result;
        }
    }
}
