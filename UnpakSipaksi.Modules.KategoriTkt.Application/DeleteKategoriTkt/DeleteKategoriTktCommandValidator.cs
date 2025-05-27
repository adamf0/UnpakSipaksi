using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.DeleteKategoriTkt
{
    public sealed class DeleteKategoriTktCommandValidator : AbstractValidator<DeleteKategoriTktCommand>
    {
        public DeleteKategoriTktCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
