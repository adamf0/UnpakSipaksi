using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Metode.Application.CreateMetode
{
    public sealed class CreateMetodeCommandValidator : AbstractValidator<CreateMetodeCommand>
    {
        public CreateMetodeCommandValidator()
        {
            RuleFor(c => c.UuidAkurasiPenelitian)
                .NotEmpty().WithMessage("'AkurasiPenelitian' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'AkurasiPenelitian' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidKejelasanPembagianTugasTim)
                .NotEmpty().WithMessage("'KejelasanPembagianTugasTim' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'KejelasanPembagianTugasTim' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidKesesuaianWaktuRabLuaranFasilitas)
                .NotEmpty().WithMessage("'KesesuaianWaktuRabLuaranFasilitas' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'KesesuaianWaktuRabLuaranFasilitas' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidPotensiKetercapaianLuaranDijanjikan)
                .NotEmpty().WithMessage("'PotensiKetercapaianLuaranDijanjikan' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'PotensiKetercapaianLuaranDijanjikan' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidModelFeasibilityStudy)
                .NotEmpty().WithMessage("'ModelFeasibilityStudy' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'ModelFeasibilityStudy' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidKesesuaianTkt)
                .NotEmpty().WithMessage("'KesesuaianTkt' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'KesesuaianTkt' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidKredibilitasMitraDukungan)
                .NotEmpty().WithMessage("'KredibilitasMitraDukungan' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'KredibilitasMitraDukungan' harus dalam format UUID v4 yang valid.");

        }
    }
}
