using FluentValidation;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.CreateKesesuaianPenugasan
{
    public sealed class CreateKesesuaianPenugasanValidator : AbstractValidator<CreateKesesuaianPenugasanCommand>
    {
        public CreateKesesuaianPenugasanValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
            RuleFor(c => c.Nilai)
              .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");

        }
    }
}
