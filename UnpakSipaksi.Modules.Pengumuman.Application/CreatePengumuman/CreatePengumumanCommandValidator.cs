using FluentValidation;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman;

//[PR] data opsional belum kena validasi
namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreatePengumuman
{
    public sealed class CreatePengumumanCommandValidator : AbstractValidator<CreatePengumumanCommand>
    {
        public CreatePengumumanCommandValidator()
        {
            RuleFor(c => c.Pesan)
                .NotEmpty().WithMessage("'Pesan' tidak boleh kosong.");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("'Type' tidak boleh kosong.");

            RuleFor(c => c.TypeExpired)
                .NotEmpty().WithMessage("'TypeExpired' tidak boleh kosong.");
        }
    }
}
