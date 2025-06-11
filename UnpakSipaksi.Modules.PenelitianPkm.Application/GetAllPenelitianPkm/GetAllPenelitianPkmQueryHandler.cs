using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllPenelitianPkm
{
    internal sealed class GetAllPenelitianPkmQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPenelitianPkmQuery, List<PenelitianPkmResponse>>
    {
        public async Task<Result<List<PenelitianPkmResponse>>> Handle(GetAllPenelitianPkmQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
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
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PenelitianPkmResponse>>(PenelitianPkmErrors.EmptyData());
            }

            IEnumerable<PenelitianPkmResponse> list = result.Select(row => new PenelitianPkmResponse
            {
                Uuid = row.Uuid?.ToString(),
                NIDN = row.NIDN.ToString(),
                Judul = row.Judul.ToString(),
                TahunPengajuan = row.TahunPengajuan.ToString(),
                KategoriProgramPengabdian = row.KategoriProgramPengabdianId?.ToString() == null ? null : new KategoriProgramPengabdianDefaultResponse(
                    row.KategoriProgramPengabdianId?.ToString(),
                    row.UuidKategoriProgramPengabdian?.ToString(),
                    row.NamaKategoriProgramPengabdian?.ToString()
                ),
                ProgramPengabdian = row.FokusPengabdianId?.ToString() == null && row.RirnId?.ToString() == null ? null : new ProgramPengabdianDefaultResponse(
                    row.FokusPengabdianId?.ToString(),
                    row.UuidFokusPengabdian?.ToString(),
                    row.NamaFokusPengabdian?.ToString(),
                    row.IdRirn?.ToString(),
                    row.UuidRirn?.ToString(),
                    row.NamaRirn?.ToString()
                ),
                RumpunIlmu = row.IdRumpunIlmu1?.ToString() == null ? null : new RumpunIlmuDefaultResponse
                {
                    IdRumpunIlmu1 = row.IdRumpunIlmu1?.ToString(),
                    UuidRumpunIlmu1 = row.UuidRumpunIlmu1?.ToString(),
                    NamaRumpunIlmu1 = row.NamaRumpunIlmu1?.ToString(),
                    IdRumpunIlmu2 = row.IdRumpunIlmu2?.ToString(),
                    UuidRumpunIlmu2 = row.UuidRumpunIlmu2?.ToString(),
                    NamaRumpunIlmu2 = row.NamaRumpunIlmu2?.ToString(),
                    IdRumpunIlmu3 = row.IdRumpunIlmu3?.ToString(),
                    UuidRumpunIlmu3 = row.UuidRumpunIlmu3?.ToString(),
                    NamaRumpunIlmu3 = row.NamaRumpunIlmu3?.ToString()
                },
                Status = row.Status.ToString(),
                Type = row.Type?.ToString(),
            });

            return Result.Success(list.ToList());
        }
    }
}
