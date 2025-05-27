

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Satuan.Application.CreateSatuan
{
    public sealed class CreateSatuanCommandValidator : AbstractValidator<CreateSatuanCommand>
    {
        public CreateSatuanCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
