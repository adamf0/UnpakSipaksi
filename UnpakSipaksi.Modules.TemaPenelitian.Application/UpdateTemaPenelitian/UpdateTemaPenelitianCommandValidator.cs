

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.UpdateTemaPenelitian
{
    public sealed class UpdateTemaPenelitianCommandValidator : AbstractValidator<UpdateTemaPenelitianCommand>
    {
        public UpdateTemaPenelitianCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.FokusPenelitianId)
                .NotEmpty().WithMessage("'FokusPenelitian' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'FokusPenelitian' harus dalam format UUID v4 yang valid.");
        }
    }
}
