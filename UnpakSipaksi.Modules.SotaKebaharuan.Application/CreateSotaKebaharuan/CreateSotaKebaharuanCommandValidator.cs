
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.CreateSotaKebaharuan
{
    public sealed class CreateSotaKebaharuanCommandValidator : AbstractValidator<CreateSotaKebaharuanCommand>
    {
        public CreateSotaKebaharuanCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
