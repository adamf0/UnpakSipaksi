using FluentValidation;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.CreateKebaruanReferensi
{
    public sealed class CreateKebaruanReferensiCommandValidator : AbstractValidator<CreateKebaruanReferensiCommand>
    {
        public CreateKebaruanReferensiCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
