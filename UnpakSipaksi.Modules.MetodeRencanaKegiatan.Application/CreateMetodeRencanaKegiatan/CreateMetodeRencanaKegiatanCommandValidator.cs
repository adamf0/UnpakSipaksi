using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreateMetodeRencanaKegiatan
{
    public sealed class CreateMetodeRencanaKegiatanCommandValidator : AbstractValidator<CreateMetodeRencanaKegiatanCommand>
    {
        public CreateMetodeRencanaKegiatanCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
