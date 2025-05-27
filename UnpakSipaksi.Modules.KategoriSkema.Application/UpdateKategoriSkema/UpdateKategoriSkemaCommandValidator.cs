using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.UpdateKategoriSkema
{
    public sealed class UpdateKategoriSkemaCommandValidator : AbstractValidator<UpdateKategoriSkemaCommand>
    {
        public UpdateKategoriSkemaCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Rule)
                .NotEmpty().WithMessage("'Rule' tidak boleh kosong.");
        }
    }
}
