

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Roadmap.Application.UpdateRoadmap
{
    public sealed class UpdateRoadmapCommandValidator : AbstractValidator<UpdateRoadmapCommand>
    {
        public UpdateRoadmapCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.Link)
                .NotEmpty().WithMessage("'Link' tidak boleh kosong.")
                .Must(c => Helper.BeAValidDriveLink(c, "drive.google.com")).WithMessage("'Link' harus berupa URL drive.google.com yang valid dan diawali dengan http:// atau https://.");
        }
    }
}
