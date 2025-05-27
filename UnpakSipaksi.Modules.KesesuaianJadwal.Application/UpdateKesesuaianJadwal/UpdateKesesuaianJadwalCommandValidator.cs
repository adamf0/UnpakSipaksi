using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.UpdateKesesuaianJadwal
{
    public sealed class UpdateKesesuaianJadwalValidator : AbstractValidator<UpdateKesesuaianJadwalCommand>
    {
        public UpdateKesesuaianJadwalValidator()
        {
            RuleFor(c => c.Uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
