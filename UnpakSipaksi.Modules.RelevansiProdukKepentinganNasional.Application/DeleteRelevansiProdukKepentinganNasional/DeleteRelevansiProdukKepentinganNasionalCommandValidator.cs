

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.DeleteRelevansiProdukKepentinganNasional
{
    public sealed class DeleteRelevansiProdukKepentinganNasionalCommandValidator : AbstractValidator<DeleteRelevansiProdukKepentinganNasionalCommand>
    {
        public DeleteRelevansiProdukKepentinganNasionalCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
