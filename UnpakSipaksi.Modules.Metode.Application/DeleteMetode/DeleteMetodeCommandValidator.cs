
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Metode.Application.DeleteMetode
{
    public sealed class DeleteMetodeCommandValidator : AbstractValidator<DeleteMetodeCommand>
    {
        public DeleteMetodeCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
