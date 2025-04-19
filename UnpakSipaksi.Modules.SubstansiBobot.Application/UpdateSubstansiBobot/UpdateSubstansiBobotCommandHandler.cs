using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SubstansiBobot.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.SubstansiBobot.Application.UpdateSubstansiBobot
{
    internal sealed class UpdateSubstansiBobotCommandHandler(
    //IUnitOfWork unitOfWork
    )
    : ICommandHandler<UpdateSubstansiBobotCommand>
    {
        public async Task<Result> Handle(UpdateSubstansiBobotCommand request, CancellationToken cancellationToken)
        {
            return Result.Success();
        }
    }
}
