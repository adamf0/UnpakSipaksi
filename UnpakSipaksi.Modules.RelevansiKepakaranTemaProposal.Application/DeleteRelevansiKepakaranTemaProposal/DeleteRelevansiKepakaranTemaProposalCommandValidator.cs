
using FluentValidation;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.DeleteRelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.DeleteRelevansiKepakaranTemaProposal
{
    public sealed class DeleteRelevansiKepakaranTemaProposalCommandValidator : AbstractValidator<DeleteRelevansiKepakaranTemaProposalCommand>
    {
        public DeleteRelevansiKepakaranTemaProposalCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
