using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Referensi.Application.UpdateReferensi
{
    public sealed class UpdateReferensiCommandValidator : AbstractValidator<UpdateReferensiCommand>
    {
        public UpdateReferensiCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.UuidKebaruanReferensi)
                .NotEmpty().WithMessage("'KebaruanReferensi' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'KebaruanReferensi' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidRelevansiKualitasReferensi)
                .NotEmpty().WithMessage("'RelevansiKualitasReferensi' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'RelevansiKualitasReferensi' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nilai)
                .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
