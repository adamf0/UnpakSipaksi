using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Substansi.Application.CreateSubstansiInternal
{
    public sealed class CreateSubstansiInternalCommandValidator : AbstractValidator<CreateSubstansiInternalCommand>
    {
        public CreateSubstansiInternalCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'PenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'PenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Komentar)
                .NotEmpty().WithMessage("'Komentar' tidak boleh kosong.");

            RuleFor(c => c.Komentar)
                .NotEmpty().WithMessage("'Komentar' tidak boleh kosong.");

            RuleFor(c => c.TanggalMulai)
                .NotEmpty().WithMessage("'TanggalMulai' tidak boleh kosong.");

            RuleFor(c => c.TanggalBerakhir)
                .NotEmpty().WithMessage("'TanggalBerakhir' tidak boleh kosong.");
        }
    }
}
