using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using IRumpunIlmu2Api = UnpakSipaksi.Modules.RumpunIlmu2.PublicApi.IRumpunIlmu2Api;
using RumpunIlmu2ResponseApi = UnpakSipaksi.Modules.RumpunIlmu2.PublicApi.RumpunIlmu2Response;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.PublicApi
{
    internal sealed class RumpunIlmu2Api(ISender sender) : IRumpunIlmu2Api
    {
        public async Task<RumpunIlmu2ResponseApi?> GetAsync(Guid RumpunIlmu2Uuid, CancellationToken cancellationToken = default)
        {
            Result<RumpunIlmu2DefaultResponse> result = await sender.Send(new GetRumpunIlmu2DefaultQuery(RumpunIlmu2Uuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RumpunIlmu2ResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
