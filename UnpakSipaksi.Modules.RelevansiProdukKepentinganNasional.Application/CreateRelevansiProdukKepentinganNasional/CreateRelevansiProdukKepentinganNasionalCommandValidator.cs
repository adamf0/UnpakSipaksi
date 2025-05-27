using FluentValidation;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.CreateRelevansiProdukKepentinganNasional
{
    public sealed class CreateRelevansiProdukKepentinganNasionalCommandValidator : AbstractValidator<CreateRelevansiProdukKepentinganNasionalCommand>
    {
        public CreateRelevansiProdukKepentinganNasionalCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
