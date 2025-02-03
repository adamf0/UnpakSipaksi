using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using IFokusPenelitianApi = UnpakSipaksi.Modules.FokusPenelitian.PublicApi.IFokusPenelitianApi;
using FokusPenelitianResponseApi= UnpakSipaksi.Modules.FokusPenelitian.PublicApi.FokusPenelitianResponse;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.PublicApi
{
    internal sealed class FokusPenelitianApi(ISender sender) : IFokusPenelitianApi
    {
        public async Task<FokusPenelitianResponseApi?> GetAsync(Guid FokusPenelitianUuid, CancellationToken cancellationToken = default)
        {
            Result<FokusPenelitianDefaultResponse> result = await sender.Send(new GetFokusPenelitianDefaultQuery(FokusPenelitianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new FokusPenelitianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
