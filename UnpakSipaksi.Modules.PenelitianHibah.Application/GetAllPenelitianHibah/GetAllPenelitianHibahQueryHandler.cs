using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllPenelitianHibah
{
    internal sealed class GetAllPenelitianHibahQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPenelitianHibahQuery, List<PenelitianHibahResponse>>
    {
        public async Task<Result<List<PenelitianHibahResponse>>> Handle(GetAllPenelitianHibahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                pi.id AS Id,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS Uuid,
                pi.NIDN AS NIDN,
                pi.judul AS Judul,
                IFNULL(DATE_FORMAT(pi.tahun_pengajuan, '%Y-%m-%d'), '') AS TahunPengajuan,
            
                pi.id_skema AS IdSkema,
                ks.uuid AS UuidSkema,
                ks.nama AS NamaSkema,
            
                pi.tkt AS TKT,
            
                pi.kategori_tkt AS IdKategoriTkt,
                kt.uuid AS UuidKategoriTkt,
                kt.nama as NamaKategoriTkt,
            
                pi.id_bidang_fokus_penelitian AS IdFokusPenelitian,
                bfp.uuid AS UuidFokusPenelitian,
                bfp.nama AS NamaFokusPenelitian,
            
                pi.id_bidang_fokus_penelitian_tema AS IdTemaPenelitian,
                bfpt.uuid AS UuidTemaPenelitian,
                bfpt.nama AS NamaTemaPenelitian,
            
                pi.id_bidang_fokus_penelitian_tema_topik AS IdTopikPenelitian,
                bfptt.uuid AS UuidTopikPenelitian,
                bfptt.nama AS NamaTemaPenelitian,
            
                pi.id_rumpun_ilmu1 AS IdRumpunIlmu1,
                ri1.uuid AS UuidRumpunIlmu1,
                ri1.nama AS NamaRumpunIlmu1,
            
                pi.id_rumpun_ilmu2 AS IdRumpunIlmu2,
                ri2.uuid AS UuidRumpunIlmu2,
                ri2.nama AS NamaRumpunIlmu2,
            
                pi.id_rumpun_ilmu3 AS IdRumpunIlmu3,
                ri3.uuid AS UuidRumpunIlmu3,
                ri3.nama AS NamaRumpunIlmu3,
            
                pi.prioritas_riset AS IdPrioritasRiset,
                pr.uuid AS UuidPrioritasRiset,
                pr.nama AS NamaPrioritasRiset,
            
                pi.lama_kegiatan AS LamaKegiatan,
                pi.status AS Status,
                pi.type AS `Type` 
            FROM penelitian_internal pi 
            LEFT JOIN kategori_skema ks ON pi.id_skema = ks.id
            LEFT JOIN kategori_tkt kt ON pi.kategori_tkt = kt.id
            LEFT JOIN bidang_fokus_penelitian bfp ON pi.id_bidang_fokus_penelitian = bfp.id
            LEFT JOIN bidang_fokus_penelitian_tema bfpt ON pi.id_bidang_fokus_penelitian_tema = bfpt.id
            LEFT JOIN bidang_fokus_penelitian_tema_topik bfptt ON pi.id_bidang_fokus_penelitian_tema_topik = bfptt.id
            LEFT JOIN rumpun_ilmu1 ri1 ON pi.id_rumpun_ilmu1 = ri1.id
            LEFT JOIN rumpun_ilmu2 ri2 ON pi.id_rumpun_ilmu2 = ri2.id
            LEFT JOIN rumpun_ilmu3 ri3 ON pi.id_rumpun_ilmu3 = ri3.id
            LEFT JOIN prioritas_riset pr ON pi.prioritas_riset = pr.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PenelitianHibahResponse>>(PenelitianHibahErrors.EmptyData());
            }

            IEnumerable<PenelitianHibahResponse> list = result.Select(row => new PenelitianHibahResponse
            {
                Uuid = row.Uuid?.ToString(),
                NIDN = row.NIDN?.ToString(),
                Judul = row.Judul?.ToString(),
                TahunPengajuan = row.TahunPengajuan?.ToString(),
                LamaKegiatan = (int?)Convert.ToInt32(row.LamaKegiatan),
                Status = row.Status?.ToString(),
                Type = row.Type?.ToString(),
                Skema = row.IdSkema?.ToString()==null? null:new SkemaResponse
                {
                    UuidSkema = row.UuidSkema?.ToString(),
                    NamaSkema = row.NamaSkema?.ToString(),
                    TKT = row.TKT?.ToString(),
                    UuidKategoriTkt = row.UuidKategoriTkt?.ToString(),
                    NamaKategoriTkt = row.NamaKategoriTkt?.ToString()
                },
                Riset = (row.IdPrioritasRiset?.ToString()==null && row.IdFokusPenelitian?.ToString()==null)? null: new RisetResponse
                {
                    UuidPrioritasRiset = row.UuidPrioritasRiset?.ToString(),
                    NamaPrioritasRiset = row.NamaPrioritasRiset?.ToString(),
                    UuidFokusPenelitian = row.UuidFokusPenelitian?.ToString(),
                    NamaFokusPenelitian = row.NamaFokusPenelitian?.ToString(),
                    UuidTemaPenelitian = row.UuidTemaPenelitian?.ToString(),
                    NamaTemaPenelitian = row.NamaTemaPenelitian?.ToString(),
                    UuidTopikPenelitian = row.UuidTopikPenelitian?.ToString(),
                    NamaTopikPenelitian = row.NamaTopikPenelitian?.ToString()
                },
                RumpunIlmu = row.IdRumpunIlmu1?.ToString()==null? null:new RumpunIlmuResponse
                {
                    UuidRumpunIlmu1 = row.UuidRumpunIlmu1?.ToString(),
                    NamaRumpunIlmu1 = row.NamaRumpunIlmu1?.ToString(),
                    UuidRumpunIlmu2 = row.UuidRumpunIlmu2?.ToString(),
                    NamaRumpunIlmu2 = row.NamaRumpunIlmu2?.ToString(),
                    UuidRumpunIlmu3 = row.UuidRumpunIlmu3?.ToString(),
                    NamaRumpunIlmu3 = row.NamaRumpunIlmu3?.ToString()
                }
            });

            return Result.Success(list.ToList());
        }
    }
}
