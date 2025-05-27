using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.DeleteKredibilitasMitraDukungan
{
    public sealed class DeleteKredibilitasMitraDukunganCommandValidator : AbstractValidator<DeleteKredibilitasMitraDukunganCommand>
    {
        public DeleteKredibilitasMitraDukunganCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
