using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.UpdateRumusanPrioritasMitra
{
    public sealed class UpdateRumusanPrioritasMitraCommandValidator : AbstractValidator<UpdateRumusanPrioritasMitraCommand>
    {
        public UpdateRumusanPrioritasMitraCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
              .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
