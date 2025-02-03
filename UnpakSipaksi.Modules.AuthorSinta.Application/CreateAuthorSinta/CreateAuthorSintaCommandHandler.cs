using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.CreateAuthorSinta
{
    internal sealed class CreateAuthorSintaCommandHandler(
    IAuthorSintaRepository authorSintaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorSintaCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateAuthorSintaCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.AuthorSinta.AuthorSinta> result = Domain.AuthorSinta.AuthorSinta.Create(
                request.Nidn,
                request.AuthorId,
                request.Score
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            authorSintaRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
