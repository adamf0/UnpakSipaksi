using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using IRumpunIlmu3Api = UnpakSipaksi.Modules.RumpunIlmu3.PublicApi.IRumpunIlmu3Api;
using RumpunIlmu3ResponseApi = UnpakSipaksi.Modules.RumpunIlmu3.PublicApi.RumpunIlmu3Response;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.PublicApi
{
    internal sealed class RumpunIlmu3Api(ISender sender) : IRumpunIlmu3Api
    {
        public async Task<RumpunIlmu3ResponseApi?> GetAsync(Guid RumpunIlmu3Uuid, CancellationToken cancellationToken = default)
        {
            Result<RumpunIlmu3DefaultResponse> result = await sender.Send(new GetRumpunIlmu3DefaultQuery(RumpunIlmu3Uuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RumpunIlmu3ResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
