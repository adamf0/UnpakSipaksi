using FluentValidation;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.CreateKejelasanPembagianTugasTim
{
    public sealed class CreateKejelasanPembagianTugasTimCommandValidator : AbstractValidator<CreateKejelasanPembagianTugasTimCommand>
    {
        public CreateKejelasanPembagianTugasTimCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
