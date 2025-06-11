using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm
{
    internal sealed class GetPenelitianPkmQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPenelitianPkmQuery, PenelitianPkmResponse>
    {
        public async Task<Result<PenelitianPkmResponse>> Handle(GetPenelitianPkmQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    pk.id AS Id,
                    CAST(NULLIF(pk.uuid, '') AS VARCHAR(36)) AS Uuid,
                    pk.NIDN AS NIDN,
                    pk.judul AS Judul,
                    IFNULL(DATE_FORMAT(pk.tahun_pengajuan, '%Y-%m-%d'), '') AS TahunPengajuan,
                
                    pk.id_kategori_program_pengabdian AS KategoriProgramPengabdianId,
                    kpp.uuid AS UuidKategoriProgramPengabdian,
                    kpp.nama AS NamaKategoriProgramPengabdian,
                
                    pk.id_fokus_pengabdian AS FokusPengabdianId,
                    fp.uuid AS UuidFokusPengabdian,
                    fp.nama as NamaFokusPengabdian,
                
                    pk.id_rirn AS RidnId,
                    r.uuid AS UuidRirn,
                    r.nama AS NamaRirn,
                
                    pk.id_rumpun_ilmu1 AS IdRumpunIlmu1,
                    ri1.uuid AS UuidRumpunIlmu1,
                    ri1.nama AS NamaRumpunIlmu1,
                
                    pk.id_rumpun_ilmu2 AS IdRumpunIlmu2,
                    ri2.uuid AS UuidRumpunIlmu2,
                    ri2.nama AS NamaRumpunIlmu2,
                
                    pk.id_rumpun_ilmu3 AS IdRumpunIlmu3,
                    ri3.uuid AS UuidRumpunIlmu3,
                    ri3.nama AS NamaRumpunIlmu3,
                
                    CASE 
                        WHEN pk.status IS TRUE THEN 1
                        WHEN pk.status IS FALSE THEN 0
                        ELSE NULL
                    END AS Status,
                    pk.type AS `Type` 
                FROM pkm pk 
                LEFT JOIN kategori_program_pengabdian kpp ON pk.id_kategori_program_pengabdian = kpp.id
                LEFT JOIN fokus_pengabdian fp ON pk.id_fokus_pengabdian = fp.id
                LEFT JOIN rirn r ON pk.id_rirn = r.id
                LEFT JOIN rumpun_ilmu1 ri1 ON pk.id_rumpun_ilmu1 = ri1.id
                LEFT JOIN rumpun_ilmu2 ri2 ON pk.id_rumpun_ilmu2 = ri2.id
                LEFT JOIN rumpun_ilmu3 ri3 ON pk.id_rumpun_ilmu3 = ri3.id
                WHERE pk.uuid = @Uuid 
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync(sql, new { Uuid = Guid.Parse(request.PenelitianPkmUuid) });
            if (result == null)
            {
                return Result.Failure<PenelitianPkmResponse>(PenelitianPkmErrors.NotFound(Guid.Parse(request.PenelitianPkmUuid)));
            }

            PenelitianPkmResponse data = new PenelitianPkmResponse
            {
                Uuid = result.Uuid?.ToString(),
                NIDN = result.NIDN.ToString(),
                Judul = result.Judul.ToString(),
                TahunPengajuan = result.TahunPengajuan.ToString(),
                KategoriProgramPengabdian = result.KategoriProgramPengabdianId?.ToString() == null ? null : new KategoriProgramPengabdianDefaultResponse(
                    result.KategoriProgramPengabdianId?.ToString(),
                    result.UuidKategoriProgramPengabdian?.ToString(),
                    result.NamaKategoriProgramPengabdian?.ToString()
                ),
                ProgramPengabdian = result.FokusPengabdianId?.ToString() == null && result.RirnId?.ToString() == null ? null : new ProgramPengabdianDefaultResponse(
                    result.FokusPengabdianId?.ToString(),
                    result.UuidFokusPengabdian?.ToString(),
                    result.NamaFokusPengabdian?.ToString(),
                    result.IdRirn?.ToString(),
                    result.UuidRirn?.ToString(),
                    result.NamaRirn?.ToString()
                ),
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
                },
                Status = result.Status.ToString(),
                Type = result.Type?.ToString(),
            };

            return data;
        }
    }
}
