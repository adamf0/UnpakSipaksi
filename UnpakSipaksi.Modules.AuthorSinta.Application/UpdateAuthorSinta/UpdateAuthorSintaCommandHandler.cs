using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.UpdateAuthorSinta
{
    internal sealed class UpdateAuthorSintaCommandHandler(
    IAuthorSintaRepository authorSintaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAuthorSintaCommand>
    {
        public async Task<Result> Handle(UpdateAuthorSintaCommand request, CancellationToken cancellationToken)
        {
            Domain.AuthorSinta.AuthorSinta? existingAuthorSinta = await authorSintaRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingAuthorSinta is null)
            {
                Result.Failure(AuthorSintaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.AuthorSinta.AuthorSinta> asset = Domain.AuthorSinta.AuthorSinta.Update(existingAuthorSinta!)
                         .ChangeNIDN(request.Nidn) 
                         .ChangeAuthorId(request.AuthorId)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
