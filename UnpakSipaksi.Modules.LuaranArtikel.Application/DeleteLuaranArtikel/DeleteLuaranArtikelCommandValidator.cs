using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.DeleteLuaranArtikel
{
    public sealed class DeleteLuaranArtikelCommandValidator : AbstractValidator<DeleteLuaranArtikelCommand>
    {
        public DeleteLuaranArtikelCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
