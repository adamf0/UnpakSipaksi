using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetBobotPublikasiDisitasiProposal
{
    internal sealed class GetBobotPublikasiDisitasiProposalQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotPublikasiDisitasiProposalQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotPublikasiDisitasiProposalQuery request, CancellationToken cancellationToken)
        {
            string column = request.KategoriSkema switch
            {
                "Penelitian Dasar" => "bobot_pdp",
                "Penelitian Dosen Pemula (PDP)" => "bobot_penelitian_dasar",
                "Penelitian Terapan" => "bobot_terapan",
                "Penelitian Kolaborasi" => "bobot_kerjasama",
                _ => string.Empty,
            };
            if (string.IsNullOrEmpty(column))
            {
                return Result.Failure<int?>(PublikasiDisitasiProposalErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM publikasi_disitasi_proposal
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(PublikasiDisitasiProposalErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(PublikasiDisitasiProposalErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
