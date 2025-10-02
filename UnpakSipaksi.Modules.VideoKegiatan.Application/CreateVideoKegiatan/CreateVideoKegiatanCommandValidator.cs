using FluentValidation;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.CreateVideoKegiatan
{
    public sealed class CreateVideoKegiatanCommandValidator : AbstractValidator<CreateVideoKegiatanCommand>
    {
        public CreateVideoKegiatanCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
             .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
