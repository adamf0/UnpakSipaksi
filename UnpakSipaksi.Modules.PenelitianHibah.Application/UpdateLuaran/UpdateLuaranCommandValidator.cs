using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLuaran
{
    public sealed class UpdateLuaranCommandValidator : AbstractValidator<UpdateLuaranCommand>
    {
        public UpdateLuaranCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidKategori)
                .NotEmpty().WithMessage("'UuidKategori' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidKategori' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidKategoriLuaran)
                .NotEmpty().WithMessage("'UuidKategoriLuaran' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidKategoriLuaran' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Jenis)
               .NotEmpty().WithMessage("'Jenis' tidak boleh kosong.")
               .Must(j => j == "wajib" || j == "tambahan").WithMessage("'Jenis' harus bernilai 'wajib' atau 'tambahan'.");
        }
    }
}
