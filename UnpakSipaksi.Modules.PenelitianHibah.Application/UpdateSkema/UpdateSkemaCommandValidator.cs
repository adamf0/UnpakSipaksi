using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSkema
{
    public sealed class UpdateSkemaCommandValidator : AbstractValidator<UpdateSkemaCommand>
    {
        public UpdateSkemaCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
               .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.SkemaId)
               .NotEmpty().WithMessage("'SkemaId' tidak boleh kosong.");

            RuleFor(c => c.TKT)
               .GreaterThanOrEqualTo(0).WithMessage("'TKT' tidak boleh negative.");

            RuleFor(c => c.KategoriTKT)
               .NotEmpty().WithMessage("'KategoriTKT' tidak boleh kosong.");
        }
    }
}
