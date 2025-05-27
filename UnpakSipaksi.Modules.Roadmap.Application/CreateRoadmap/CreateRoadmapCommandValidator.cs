
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Roadmap.Application.CreateRoadmap
{
    public sealed class CreateRoadmapCommandValidator : AbstractValidator<CreateRoadmapCommand>
    {
        public CreateRoadmapCommandValidator()
        {
            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.Link)
                .NotEmpty().WithMessage("'Link' tidak boleh kosong.")
                .Must(c => Helper.BeAValidDriveLink(c, "drive.google.com")).WithMessage("'Link' harus berupa URL drive.google.com yang valid dan diawali dengan http:// atau https://.");
        }
    }
}
