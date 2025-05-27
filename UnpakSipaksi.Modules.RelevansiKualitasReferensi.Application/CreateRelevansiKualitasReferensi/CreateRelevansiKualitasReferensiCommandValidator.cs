using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.CreateRelevansiKualitasReferensi
{
    public sealed class CreateRelevansiKualitasReferensiCommandValidator : AbstractValidator<CreateRelevansiKualitasReferensiCommand>
    {
        public CreateRelevansiKualitasReferensiCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
