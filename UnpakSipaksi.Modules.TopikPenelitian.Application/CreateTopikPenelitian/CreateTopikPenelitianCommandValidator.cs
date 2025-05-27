

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.CreateTopikPenelitian
{
    public sealed class CreateTopikPenelitianCommandValidator : AbstractValidator<CreateTopikPenelitianCommand>
    {
        public CreateTopikPenelitianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.TemaPenelitianId)
                .NotEmpty().WithMessage("'TemaPenelitian' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'TemaPenelitian' harus dalam format UUID v4 yang valid.");
        }
    }
}
