using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Logbook.Application.CreateLogbook
{
    public sealed class CreateLogbookCommandValidator : AbstractValidator<CreateLogbookCommand>
    {
        public bool BeValidReffence(string? UuidPenelitianHibah, string? UuidPenelitianPkm)
        {
            return string.IsNullOrEmpty(UuidPenelitianHibah) && string.IsNullOrEmpty(UuidPenelitianPkm);
        }
        public CreateLogbookCommandValidator()
        {
            RuleFor(c => c)
                .Must(c => BeValidReffence(c.UuidPenelitianHibah, c.UuidPenelitianPkm))
                .WithMessage("'Hibah' tidak boleh kosong");

            RuleFor(c => c.TanggalKegiatan)
                .NotEmpty().WithMessage("'TanggalKegiatan' tidak boleh kosong.");

            RuleFor(c => c.Lampiran)
                .NotEmpty().WithMessage("'Lampiran' tidak boleh kosong.");

            RuleFor(c => c.Deskripsi)
                .NotEmpty().WithMessage("'Deskripsi' tidak boleh kosong.");

            RuleFor(c => c.Persentase)
                .NotEmpty().WithMessage("'Persentase' tidak boleh kosong.");
        }
    }
}
