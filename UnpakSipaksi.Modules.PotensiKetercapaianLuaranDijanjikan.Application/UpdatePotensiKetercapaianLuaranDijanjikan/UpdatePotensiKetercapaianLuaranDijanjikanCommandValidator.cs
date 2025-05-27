
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.UpdatePotensiKetercapaianLuaranDijanjikan
{
    public sealed class UpdatePotensiKetercapaianLuaranDijanjikanCommandValidator : AbstractValidator<UpdatePotensiKetercapaianLuaranDijanjikanCommand>
    {
        public UpdatePotensiKetercapaianLuaranDijanjikanCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
