
using FluentValidation;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.CreateRoadmapPenelitian
{
    public sealed class CreateRoadmapPenelitianCommandValidator : AbstractValidator<CreateRoadmapPenelitianCommand>
    {
        public CreateRoadmapPenelitianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
