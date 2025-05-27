
using FluentValidation;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.CreatePrioritasRiset
{
    public sealed class CreatePrioritasRisetCommandValidator : AbstractValidator<CreatePrioritasRisetCommand>
    {
        public CreatePrioritasRisetCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
