using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.DeleteVideoKegiatan
{
    public sealed class DeleteVideoKegiatanCommandValidator : AbstractValidator<DeleteVideoKegiatanCommand>
    {
        public DeleteVideoKegiatanCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
