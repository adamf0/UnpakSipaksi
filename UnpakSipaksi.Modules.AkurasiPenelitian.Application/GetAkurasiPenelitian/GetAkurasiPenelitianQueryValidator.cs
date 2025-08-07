using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian
{
    public sealed class GetAkurasiPenelitianQueryValidator : AbstractValidator<GetAkurasiPenelitianQuery>
    {
        public GetAkurasiPenelitianQueryValidator()
        {
            RuleFor(c => c.AkurasiPenelitianUuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
