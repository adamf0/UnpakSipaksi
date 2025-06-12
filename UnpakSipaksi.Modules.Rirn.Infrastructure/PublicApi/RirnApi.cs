using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using IRirnApi = UnpakSipaksi.Modules.Rirn.PublicApi.IRirnApi;
using RirnResponseApi= UnpakSipaksi.Modules.Rirn.PublicApi.RirnResponse;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;

namespace UnpakSipaksi.Modules.Rirn.Infrastructure.PublicApi
{
    internal sealed class RirnApi(ISender sender) : IRirnApi
    {
        public async Task<RirnResponseApi?> GetAsync(Guid RirnUuid, CancellationToken cancellationToken = default)
        {
            Result<RirnDefaultResponse> result = await sender.Send(new GetRirnDefaultQuery(RirnUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RirnResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
