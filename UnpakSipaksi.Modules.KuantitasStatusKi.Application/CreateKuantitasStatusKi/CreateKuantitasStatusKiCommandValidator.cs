using FluentValidation;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.CreateKuantitasStatusKi
{
    public sealed class CreateKuantitasStatusKiCommandValidator : AbstractValidator<CreateKuantitasStatusKiCommand>
    {
        public CreateKuantitasStatusKiCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
               .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
