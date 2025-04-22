using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Application.GetAllInsentif
{
    internal sealed class GetAllInsentifQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllInsentifQuery, List<InsentifResponse>>
    {
        public async Task<Result<List<InsentifResponse>>> Handle(GetAllInsentifQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            LEFT JOIN jenis_publikasi jp ON i.id_jenis_publikasi = jp.id;
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<InsentifResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<InsentifResponse>>(InsentifErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
