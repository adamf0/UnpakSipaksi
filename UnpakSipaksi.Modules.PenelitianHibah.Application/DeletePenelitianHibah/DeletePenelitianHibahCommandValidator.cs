using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeletePenelitianHibah
{
    public sealed class DeletePenelitianHibahCommandValidator : AbstractValidator<DeletePenelitianHibahCommand>
    {
        public DeletePenelitianHibahCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
