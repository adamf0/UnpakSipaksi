using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberMahasiswa
{
    public sealed class CreateMemberMahasiswaCommandValidator : AbstractValidator<CreateMemberMahasiswaCommand>
    {
        public CreateMemberMahasiswaCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.NPM)
                .NotEmpty().WithMessage("'NPM' tidak boleh kosong.")
                .Must(Helper.BeValidNPM).WithMessage("'NPM' tidak valid.");
        }
    }
}
