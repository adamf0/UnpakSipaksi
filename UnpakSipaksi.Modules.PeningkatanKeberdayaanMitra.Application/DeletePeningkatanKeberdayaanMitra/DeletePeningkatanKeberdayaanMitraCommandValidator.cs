
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.DeletePeningkatanKeberdayaanMitra
{
    public sealed class DeletePeningkatanKeberdayaanMitraCommandValidator : AbstractValidator<DeletePeningkatanKeberdayaanMitraCommand>
    {
        public DeletePeningkatanKeberdayaanMitraCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
