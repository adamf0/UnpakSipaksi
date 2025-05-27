using FluentValidation;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.CreateKesesuaianSolusiMasalahMitra
{
    public sealed class CreateKesesuaianSolusiMasalahMitraValidator : AbstractValidator<CreateKesesuaianSolusiMasalahMitraCommand>
    {
        public CreateKesesuaianSolusiMasalahMitraValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
