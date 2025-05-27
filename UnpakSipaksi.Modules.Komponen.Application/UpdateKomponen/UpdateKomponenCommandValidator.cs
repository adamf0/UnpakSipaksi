using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Komponen.Application.UpdateKomponen
{
    public sealed class UpdateKomponenCommandValidator : AbstractValidator<UpdateKomponenCommand>
    {
        public UpdateKomponenCommandValidator()
        {
            RuleFor(c => c.Uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.MaxBiaya)
                .GreaterThanOrEqualTo(0).WithMessage("'MaxBiaya' tidak boleh kurang dari 0.");
                //.LessThan(0).WithMessage("'MaxBiaya' tidak boleh kurang dari 0");
        }
    }
}
