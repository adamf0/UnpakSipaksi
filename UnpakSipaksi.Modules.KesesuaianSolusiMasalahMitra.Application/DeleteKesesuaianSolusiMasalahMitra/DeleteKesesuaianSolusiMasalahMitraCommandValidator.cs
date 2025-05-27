using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.DeleteKesesuaianSolusiMasalahMitra
{
    public sealed class DeleteKesesuaianSolusiMasalahMitraValidator : AbstractValidator<DeleteKesesuaianSolusiMasalahMitraCommand>
    {
        public DeleteKesesuaianSolusiMasalahMitraValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
