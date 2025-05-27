using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.DeleteKesesuaianTkt
{
    public sealed class DeleteKesesuaianSolusiMasalahMitraValidator : AbstractValidator<DeleteKesesuaianTktCommand>
    {
        public DeleteKesesuaianSolusiMasalahMitraValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
