

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Roadmap.Application.DeleteRoadmap
{
    public sealed class DeleteRoadmapCommandValidator : AbstractValidator<DeleteRoadmapCommand>
    {
        public DeleteRoadmapCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
