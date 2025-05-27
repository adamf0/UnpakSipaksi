using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.UpdatePenugasanReviewer
{
    public sealed class UpdatePenugasanReviewerCommandValidator : AbstractValidator<UpdatePenugasanReviewerCommand>
    {
        public UpdatePenugasanReviewerCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("'Status' tidak boleh kosong.");
        }
    }
}
