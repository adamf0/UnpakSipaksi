using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetAllPublikasiDisitasiProposal
{
    internal sealed class GetAllPublikasiDisitasiProposalQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPublikasiDisitasiProposalQuery, List<PublikasiDisitasiProposalResponse>>
    {
        public async Task<Result<List<PublikasiDisitasiProposalResponse>>> Handle(GetAllPublikasiDisitasiProposalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                bobot_pdp AS BobotPDP,
                bobot_terapan AS BobotTerapan,
                bobot_kerjasama AS BobotKerjasama,
                bobot_penelitian_dasar AS BobotPenelitianDasar,
                skor AS Skor 
            FROM publikasi_disitasi_proposal
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<PublikasiDisitasiProposalResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PublikasiDisitasiProposalResponse>>(PublikasiDisitasiProposalErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
