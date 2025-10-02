using FluentValidation;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.CreateLuaranArtikel
{
    public sealed class CreateLuaranArtikelCommandValidator : AbstractValidator<CreateLuaranArtikelCommand>
    {
        public CreateLuaranArtikelCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
               .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
