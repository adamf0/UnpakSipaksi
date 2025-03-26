using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using IRumpunIlmu1Api = UnpakSipaksi.Modules.RumpunIlmu1.PublicApi.IRumpunIlmu1Api;
using RumpunIlmu1ResponseApi = UnpakSipaksi.Modules.RumpunIlmu1.PublicApi.RumpunIlmu1Response;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.PublicApi
{
    internal sealed class RumpunIlmu1Api(ISender sender) : IRumpunIlmu1Api
    {
        public async Task<RumpunIlmu1ResponseApi?> GetAsync(Guid RumpunIlmu1Uuid, CancellationToken cancellationToken = default)
        {
            Result<RumpunIlmu1DefaultResponse> result = await sender.Send(new GetRumpunIlmu1DefaultQuery(RumpunIlmu1Uuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RumpunIlmu1ResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
