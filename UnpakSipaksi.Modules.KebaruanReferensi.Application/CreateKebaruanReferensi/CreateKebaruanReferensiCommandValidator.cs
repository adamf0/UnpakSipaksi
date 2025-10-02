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
               .GreaterThanOrEqualTo(0).WithMessage("'Skor' tidak boleh negative.");
        }
    }
}
