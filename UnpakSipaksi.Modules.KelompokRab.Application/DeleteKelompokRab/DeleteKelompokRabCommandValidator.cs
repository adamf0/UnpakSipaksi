using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KelompokRab.Application.DeleteKelompokRab
{
    public sealed class DeleteKelompokRabCommandValidator : AbstractValidator<DeleteKelompokRabCommand>
    {
        public DeleteKelompokRabCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
