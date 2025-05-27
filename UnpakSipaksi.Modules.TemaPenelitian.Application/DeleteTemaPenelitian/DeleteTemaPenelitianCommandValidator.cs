

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.DeleteTemaPenelitian
{
    public sealed class DeleteTemaPenelitianCommandValidator : AbstractValidator<DeleteTemaPenelitianCommand>
    {
        public DeleteTemaPenelitianCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
