using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Common.Application.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
}
