using FluentValidation;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.CreateKredibilitasMitraDukungan
{
    public sealed class CreateKredibilitasMitraDukunganCommandValidator : AbstractValidator<CreateKredibilitasMitraDukunganCommand>
    {
        public CreateKredibilitasMitraDukunganCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .GreaterThanOrEqualTo(0).WithMessage("'Skor' tidak boleh negative.");
        }
    }
}
