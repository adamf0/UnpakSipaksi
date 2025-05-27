


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.DeleteTopikPenelitian
{
    public sealed class DeleteTopikPenelitianCommandValidator : AbstractValidator<DeleteTopikPenelitianCommand>
    {
        public DeleteTopikPenelitianCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
