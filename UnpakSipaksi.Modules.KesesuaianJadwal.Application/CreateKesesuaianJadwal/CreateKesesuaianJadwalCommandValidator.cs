using FluentValidation;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.CreateKesesuaianJadwal
{
    public sealed class CreateKesesuaianJadwalValidator : AbstractValidator<CreateKesesuaianJadwalCommand>
    {
        public CreateKesesuaianJadwalValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
