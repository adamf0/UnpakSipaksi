using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.DeleteKategoriSkema
{
    public sealed class DeleteKategoriSkemaCommandValidator : AbstractValidator<DeleteKategoriSkemaCommand>
    {
        public DeleteKategoriSkemaCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
