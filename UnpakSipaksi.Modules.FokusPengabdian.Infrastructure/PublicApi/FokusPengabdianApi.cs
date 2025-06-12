using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using IFokusPengabdianApi = UnpakSipaksi.Modules.FokusPengabdian.PublicApi.IFokusPengabdianApi;
using FokusPengabdianResponseApi= UnpakSipaksi.Modules.FokusPengabdian.PublicApi.FokusPengabdianResponse;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.PublicApi
{
    internal sealed class FokusPengabdianApi(ISender sender) : IFokusPengabdianApi
    {
        public async Task<FokusPengabdianResponseApi?> GetAsync(Guid FokusPengabdianUuid, CancellationToken cancellationToken = default)
        {
            Result<FokusPengabdianDefaultResponse> result = await sender.Send(new GetFokusPengabdianDefaultQuery(FokusPengabdianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new FokusPengabdianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
