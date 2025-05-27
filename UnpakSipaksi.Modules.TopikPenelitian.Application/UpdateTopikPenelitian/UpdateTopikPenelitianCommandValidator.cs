

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.UpdateTopikPenelitian
{
    public sealed class UpdateTopikPenelitianCommandValidator : AbstractValidator<UpdateTopikPenelitianCommand>
    {
        public UpdateTopikPenelitianCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.TemaPenelitianId)
                .NotEmpty().WithMessage("'TemaPenelitian' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'TemaPenelitian' harus dalam format UUID v4 yang valid.");
        }
    }
}
