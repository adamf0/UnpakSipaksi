using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Komponen.Application.DeleteKomponen
{
    public sealed class DeleteKomponenCommandValidator : AbstractValidator<DeleteKomponenCommand>
    {
        public DeleteKomponenCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
