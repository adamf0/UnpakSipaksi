using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteDokumenKontrak
{
    public sealed class DeleteDokumenKontrakCommandValidator : AbstractValidator<DeleteDokumenKontrakCommand>
    {
        public DeleteDokumenKontrakCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
