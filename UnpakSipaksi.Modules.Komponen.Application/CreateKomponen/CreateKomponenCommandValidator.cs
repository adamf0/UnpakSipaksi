using FluentValidation;

namespace UnpakSipaksi.Modules.Komponen.Application.CreateKomponen
{
    public sealed class CreateKomponenCommandValidator : AbstractValidator<CreateKomponenCommand>
    {
        public CreateKomponenCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.MaxBiaya)
                .GreaterThanOrEqualTo(0).WithMessage("'MaxBiaya' tidak boleh kurang dari 0.");
                //.LessThan(0).WithMessage("'MaxBiaya' tidak boleh kurang dari 0");
        }
    }
}
