
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.DeletePenugasanReviewer
{
    public sealed class DeletePenugasanReviewerCommandValidator : AbstractValidator<DeletePenugasanReviewerCommand>
    {
        public DeletePenugasanReviewerCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
