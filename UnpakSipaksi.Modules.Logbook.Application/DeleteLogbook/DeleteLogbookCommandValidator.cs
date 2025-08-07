using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Logbook.Application.DeleteLogbook
{
    public sealed class DeleteLogbookCommandValidator : AbstractValidator<DeleteLogbookCommand>
    {
        public bool BeValidReffence(string? UuidPenelitianHibah, string? UuidPenelitianPkm)
        {
            return string.IsNullOrEmpty(UuidPenelitianHibah) && string.IsNullOrEmpty(UuidPenelitianPkm);
        }
        public DeleteLogbookCommandValidator()
        {
            RuleFor(c => c.Uuid)
              .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
              .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c)
                .Must(c => BeValidReffence(c.UuidPenelitianHibah, c.UuidPenelitianPkm))
                .WithMessage("'Hibah' tidak boleh kosong");
        }
    }
}
