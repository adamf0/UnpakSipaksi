﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Common.Application.Messaging
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand;

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>;
}
