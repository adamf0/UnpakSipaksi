using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah
{
    internal sealed class GetPenelitianHibahDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPenelitianHibahDefaultQuery, PenelitianHibahDefaultResponse>
    {
        public async Task<Result<PenelitianHibahDefaultResponse>> Handle(GetPenelitianHibahDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
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
                 WHERE pi.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync(sql, new { Uuid = request.PenelitianHibahUuid });
            if (result == null)
            {
                return Result.Failure<PenelitianHibahDefaultResponse>(PenelitianHibahErrors.NotFound(request.PenelitianHibahUuid));
            }

            PenelitianHibahDefaultResponse data = new PenelitianHibahDefaultResponse
            {
                Id = result.Id?.ToString(),
                Uuid = result.Uuid?.ToString(),
                NIDN = result.NIDN?.ToString(),
                Judul = result.Judul?.ToString(),
                TahunPengajuan = result.TahunPengajuan?.ToString(),
                LamaKegiatan = (int?)Convert.ToInt32(result.LamaKegiatan),
                Status = result.Status?.ToString(),
                Type = result.Type?.ToString(),
                Skema = result.IdSkema?.ToString() == null ? null : new SkemaDefaultResponse
                {
                    IdSkema = result.IdSkema?.ToString(),
                    UuidSkema = result.UuidSkema?.ToString(),
                    NamaSkema = result.NamaSkema?.ToString(),
                    TKT = result.TKT?.ToString(),
                    IdKategoriTkt = result.IdKategoriTkt?.ToString(),
                    UuidKategoriTkt = result.UuidKategoriTkt?.ToString(),
                    NamaKategoriTkt = result.NamaKategoriTkt?.ToString()
                },
                Riset = (result.IdPrioritasRiset?.ToString() == null && result.IdFokusPenelitian?.ToString() == null) ? null : new RisetDefaultResponse
                {
                    IdPrioritasRiset = result.IdPrioritasRiset?.ToString(),
                    UuidPrioritasRiset = result.UuidPrioritasRiset?.ToString(),
                    NamaPrioritasRiset = result.NamaPrioritasRiset?.ToString(),
                    IdFokusPenelitian = result.IdFokusPenelitian?.ToString(),
                    UuidFokusPenelitian = result.UuidFokusPenelitian?.ToString(),
                    NamaFokusPenelitian = result.NamaFokusPenelitian?.ToString(),
                    IdTemaPenelitian = result.IdTemaPenelitian?.ToString(),
                    UuidTemaPenelitian = result.UuidTemaPenelitian?.ToString(),
                    NamaTemaPenelitian = result.NamaTemaPenelitian?.ToString(),
                    IdTopikPenelitian = result.IdTopikPenelitian?.ToString(),
                    UuidTopikPenelitian = result.UuidTopikPenelitian?.ToString(),
                    NamaTopikPenelitian = result.NamaTopikPenelitian?.ToString()
                },
                RumpunIlmu = result.IdRumpunIlmu1?.ToString() == null ? null : new RumpunIlmuDefaultResponse
                {
                    IdRumpunIlmu1 = result.IdRumpunIlmu1?.ToString(),
                    UuidRumpunIlmu1 = result.UuidRumpunIlmu1?.ToString(),
                    NamaRumpunIlmu1 = result.NamaRumpunIlmu1?.ToString(),
                    IdRumpunIlmu2 = result.IdRumpunIlmu2?.ToString(),
                    UuidRumpunIlmu2 = result.UuidRumpunIlmu2?.ToString(),
                    NamaRumpunIlmu2 = result.NamaRumpunIlmu2?.ToString(),
                    IdRumpunIlmu3 = result.IdRumpunIlmu3?.ToString(),
                    UuidRumpunIlmu3 = result.UuidRumpunIlmu3?.ToString(),
                    NamaRumpunIlmu3 = result.NamaRumpunIlmu3?.ToString()
                }
            };

            return data;
        }
    }
}
