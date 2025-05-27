


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Satuan.Application.DeleteSatuan
{
    public sealed class DeleteSatuanCommandValidator : AbstractValidator<DeleteSatuanCommand>
    {
        public DeleteSatuanCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
