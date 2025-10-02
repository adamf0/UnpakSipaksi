using FluentValidation;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Modules.Pengumuman.Application.DeletePengumuman;

//[PR] data opsional belum kena validasi
namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreatePengumuman
{
    public sealed class DeletePengumumanCommandValidator : AbstractValidator<DeletePengumumanCommand>
    {
        public DeletePengumumanCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
