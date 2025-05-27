

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.DeleteRoadmapPenelitian
{
    public sealed class DeleteRoadmapPenelitianCommandValidator : AbstractValidator<DeleteRoadmapPenelitianCommand>
    {
        public DeleteRoadmapPenelitianCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
