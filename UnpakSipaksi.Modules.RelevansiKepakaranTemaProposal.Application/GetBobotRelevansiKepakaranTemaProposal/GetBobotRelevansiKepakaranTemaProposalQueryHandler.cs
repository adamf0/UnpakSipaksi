using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetBobotRelevansiKepakaranTemaProposal
{
    internal sealed class GetBobotRelevansiKepakaranTemaProposalQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotRelevansiKepakaranTemaProposalQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotRelevansiKepakaranTemaProposalQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(RelevansiKepakaranTemaProposalErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM relevansi_kepakaran_tema_proposal
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(RelevansiKepakaranTemaProposalErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(RelevansiKepakaranTemaProposalErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
