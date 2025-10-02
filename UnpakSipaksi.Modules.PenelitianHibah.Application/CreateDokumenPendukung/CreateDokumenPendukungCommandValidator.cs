using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenPendukung
{
    public sealed class CreateDokumenPendukungCommandValidator : AbstractValidator<CreateDokumenPendukungCommand>
    {
        public CreateDokumenPendukungCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.File) || !string.IsNullOrEmpty(x.Link))
                .WithMessage("'File' atau 'Link' harus diisi.");

            RuleFor(x => x)
                .Must(x => string.IsNullOrEmpty(x.File) || string.IsNullOrEmpty(x.Link))
                .WithMessage("'File' dan 'Link' tidak boleh diisi bersamaan.");

            //RuleFor(c => c.Kategori)
            //   .NotEmpty().WithMessage("'Kategori' tidak boleh kosong.");
        }
    }
}
