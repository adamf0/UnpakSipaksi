using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateRAB
{
    public sealed class CreateRABCommandValidator : AbstractValidator<CreateRABCommand>
    {
        public CreateRABCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Total)
               .NotNull()
               .WithMessage("'Total' harus diisi jika Item atau Harga Satuan diisi.")
               .When(c => c.Item.HasValue || c.HargaSatuan.HasValue);

            RuleFor(c => c.Total)
                .Null()
                .WithMessage("'Total' harus kosong jika Item dan Harga Satuan kosong.")
                .When(c => !c.Item.HasValue && !c.HargaSatuan.HasValue);
        }
    }
}
