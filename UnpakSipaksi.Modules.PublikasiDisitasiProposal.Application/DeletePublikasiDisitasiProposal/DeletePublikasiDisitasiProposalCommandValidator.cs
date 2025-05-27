


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.DeletePublikasiDisitasiProposal
{
    public sealed class DeletePublikasiDisitasiProposalCommandValidator : AbstractValidator<DeletePublikasiDisitasiProposalCommand>
    {
        public DeletePublikasiDisitasiProposalCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
