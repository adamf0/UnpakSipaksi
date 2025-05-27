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
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
