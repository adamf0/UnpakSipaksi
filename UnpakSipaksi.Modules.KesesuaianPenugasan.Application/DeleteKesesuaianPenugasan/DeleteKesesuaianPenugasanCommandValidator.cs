using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.DeleteKesesuaianPenugasan
{
    public sealed class DeleteKesesuaianPenugasanValidator : AbstractValidator<DeleteKesesuaianPenugasanCommand>
    {
        public DeleteKesesuaianPenugasanValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
