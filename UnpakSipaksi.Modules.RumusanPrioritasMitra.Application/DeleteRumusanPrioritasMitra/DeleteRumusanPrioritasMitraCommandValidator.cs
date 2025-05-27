

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.DeleteRumusanPrioritasMitra
{
    public sealed class DeleteRumusanPrioritasMitraCommandValidator : AbstractValidator<DeleteRumusanPrioritasMitraCommand>
    {
        public DeleteRumusanPrioritasMitraCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
