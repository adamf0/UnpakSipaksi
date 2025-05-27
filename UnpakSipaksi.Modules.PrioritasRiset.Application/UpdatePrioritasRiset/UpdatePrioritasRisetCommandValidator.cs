
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.UpdatePrioritasRiset
{
    public sealed class UpdatePrioritasRisetCommandValidator : AbstractValidator<UpdatePrioritasRisetCommand>
    {
        public UpdatePrioritasRisetCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
