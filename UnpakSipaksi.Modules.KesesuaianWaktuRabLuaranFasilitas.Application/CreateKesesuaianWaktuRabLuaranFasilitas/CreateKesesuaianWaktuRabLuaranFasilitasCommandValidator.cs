using FluentValidation;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.CreateKesesuaianWaktuRabLuaranFasilitas
{
    public sealed class CreateKesesuaianWaktuRabLuaranFasilitasValidator : AbstractValidator<CreateKesesuaianWaktuRabLuaranFasilitasCommand>
    {
        public CreateKesesuaianWaktuRabLuaranFasilitasValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
              .GreaterThanOrEqualTo(0).WithMessage("'Skor' tidak boleh negative.");
        }
    }
}
