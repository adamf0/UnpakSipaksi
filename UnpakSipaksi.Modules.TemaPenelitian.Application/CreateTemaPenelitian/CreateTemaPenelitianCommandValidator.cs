

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.CreateTemaPenelitian
{
    public sealed class CreateTemaPenelitianCommandValidator : AbstractValidator<CreateTemaPenelitianCommand>
    {
        public CreateTemaPenelitianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.FokusPenelitianId)
                .NotEmpty().WithMessage("'FokusPenelitian' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'FokusPenelitian' harus dalam format UUID v4 yang valid.");
        }
    }
}
