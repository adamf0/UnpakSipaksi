using FluentValidation;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.CreatePenugasanReviewer
{
    public sealed class CreatePenugasanReviewerCommandValidator : AbstractValidator<CreatePenugasanReviewerCommand>
    {
        public CreatePenugasanReviewerCommandValidator()
        {
            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("'Status' tidak boleh kosong.");
        }
    }
}
