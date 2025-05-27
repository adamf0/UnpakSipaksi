using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.DeleteKesesuaianJadwal
{
    public sealed class DeleteKesesuaianJadwalValidator : AbstractValidator<DeleteKesesuaianJadwalCommand>
    {
        public DeleteKesesuaianJadwalValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
