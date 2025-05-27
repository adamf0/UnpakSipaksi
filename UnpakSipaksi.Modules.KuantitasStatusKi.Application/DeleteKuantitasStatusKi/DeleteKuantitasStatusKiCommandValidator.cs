using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.DeleteKuantitasStatusKi
{
    public sealed class DeleteKuantitasStatusKiCommandValidator : AbstractValidator<DeleteKuantitasStatusKiCommand>
    {
        public DeleteKuantitasStatusKiCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
