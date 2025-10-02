using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberMahasiswa
{
    public sealed class DeleteMemberMahasiswaCommandValidator : AbstractValidator<DeleteMemberMahasiswaCommand>
    {
        public DeleteMemberMahasiswaCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
