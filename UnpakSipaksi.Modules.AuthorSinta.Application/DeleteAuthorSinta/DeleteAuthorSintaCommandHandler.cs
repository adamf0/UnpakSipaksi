using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.DeleteAuthorSinta
{
    internal sealed class DeleteAuthorSintaCommandHandler(
    IAuthorSintaRepository authorSintaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteAuthorSintaCommand>
    {
        public async Task<Result> Handle(DeleteAuthorSintaCommand request, CancellationToken cancellationToken)
        {
            Domain.AuthorSinta.AuthorSinta? existingAuthorSinta = await authorSintaRepository.GetAsync(request.uuid, cancellationToken);

            if (existingAuthorSinta is null)
            {
                return Result.Failure(AuthorSintaErrors.NotFound(request.uuid));
            }

            await authorSintaRepository.DeleteAsync(existingAuthorSinta!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
