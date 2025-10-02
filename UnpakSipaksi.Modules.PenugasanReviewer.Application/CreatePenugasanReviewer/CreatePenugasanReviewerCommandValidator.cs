using FluentValidation;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.CreatePenugasanReviewer
{
    //[PR] belum di test BeValidNidn
    public sealed class CreatePenugasanReviewerCommandValidator : AbstractValidator<CreatePenugasanReviewerCommand>
    {
        public CreatePenugasanReviewerCommandValidator()
        {
            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.Status)
                .InclusiveBetween(0, 1)
                .WithMessage("'Status' format tidak diketahui.");
        }
    }
}
