using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JenisLuaran.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.CreateJenisLuaran
{
    internal sealed class CreateJenisLuaranCommandHandler(
    IJenisLuaranRepository JenisLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateJenisLuaranCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateJenisLuaranCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.JenisLuaran> result = Domain.JenisLuaran.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            JenisLuaranRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
