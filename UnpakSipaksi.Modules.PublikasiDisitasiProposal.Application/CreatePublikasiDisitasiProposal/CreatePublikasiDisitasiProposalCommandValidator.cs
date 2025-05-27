

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.CreatePublikasiDisitasiProposal
{
    public sealed class CreatePublikasiDisitasiProposalCommandValidator : AbstractValidator<CreatePublikasiDisitasiProposalCommand>
    {
        public CreatePublikasiDisitasiProposalCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");

        }
    }
}
