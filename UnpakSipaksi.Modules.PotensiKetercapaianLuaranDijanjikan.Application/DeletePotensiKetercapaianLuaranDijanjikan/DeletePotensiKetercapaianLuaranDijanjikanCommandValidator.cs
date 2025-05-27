
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.DeletePotensiKetercapaianLuaranDijanjikan
{
    public sealed class DeletePotensiKetercapaianLuaranDijanjikanCommandValidator : AbstractValidator<DeletePotensiKetercapaianLuaranDijanjikanCommand>
    {
        public DeletePotensiKetercapaianLuaranDijanjikanCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
