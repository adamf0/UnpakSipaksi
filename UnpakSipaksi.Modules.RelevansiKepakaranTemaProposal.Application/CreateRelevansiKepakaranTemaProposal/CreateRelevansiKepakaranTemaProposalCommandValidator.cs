using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.CreateRelevansiKepakaranTemaProposal
{
    public sealed class CreateRelevansiKepakaranTemaProposalCommandValidator : AbstractValidator<CreateRelevansiKepakaranTemaProposalCommand>
    {
        public CreateRelevansiKepakaranTemaProposalCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
               .GreaterThanOrEqualTo(0).WithMessage("'Skor' tidak boleh negative.");
        }
    }
}
