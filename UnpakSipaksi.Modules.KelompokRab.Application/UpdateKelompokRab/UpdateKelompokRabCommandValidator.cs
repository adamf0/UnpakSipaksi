using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KelompokRab.Application.UpdateKelompokRab
{
    public sealed class UpdateKelompokRabCommandValidator : AbstractValidator<UpdateKelompokRabCommand>
    {
        public UpdateKelompokRabCommandValidator()
        {
            RuleFor(c => c.Uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
