using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.DeleteLuaranHibah
{
    public sealed class DeleteLuaranHibahCommandValidator : AbstractValidator<DeleteLuaranHibahCommand>
    {
        public DeleteLuaranHibahCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
