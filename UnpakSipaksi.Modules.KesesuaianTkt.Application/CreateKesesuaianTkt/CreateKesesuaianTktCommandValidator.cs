using FluentValidation;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.CreateKesesuaianTkt
{
    public sealed class CreateKesesuaianSolusiMasalahMitraValidator : AbstractValidator<CreateKesesuaianTktCommand>
    {
        public CreateKesesuaianSolusiMasalahMitraValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
              .GreaterThanOrEqualTo(0).WithMessage("'Skor' tidak boleh negative.");
        }
    }
}
