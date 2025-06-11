using FluentValidation;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman;
using UnpakSipaksi.Modules.Pengumuman.Application.UpdatePengumuman;

//[PR] data opsional belum kena validasi
namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreatePengumuman
{
    public sealed class UpdatePengumumanCommandValidator : AbstractValidator<UpdatePengumumanCommand>
    {
        public UpdatePengumumanCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Pesan)
                .NotEmpty().WithMessage("'Pesan' tidak boleh kosong.");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("'Type' tidak boleh kosong.");

            RuleFor(c => c.TypeExpired)
                .NotEmpty().WithMessage("'TypeExpired' tidak boleh kosong.");
        }
    }
}
