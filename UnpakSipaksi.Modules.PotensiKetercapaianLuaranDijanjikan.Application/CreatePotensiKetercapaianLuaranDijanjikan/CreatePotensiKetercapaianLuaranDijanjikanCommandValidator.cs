
using FluentValidation;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.CreatePotensiKetercapaianLuaranDijanjikan
{
    public sealed class CreatePotensiKetercapaianLuaranDijanjikanCommandValidator : AbstractValidator<CreatePotensiKetercapaianLuaranDijanjikanCommand>
    {
        public CreatePotensiKetercapaianLuaranDijanjikanCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
               .GreaterThanOrEqualTo(0).WithMessage("'Skor' tidak boleh negative.");
        }
    }
}
