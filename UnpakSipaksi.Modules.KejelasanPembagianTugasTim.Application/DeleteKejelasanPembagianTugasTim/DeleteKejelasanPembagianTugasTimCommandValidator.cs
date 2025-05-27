using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.DeleteKejelasanPembagianTugasTim
{
    public sealed class DeleteKejelasanPembagianTugasTimCommandValidator : AbstractValidator<DeleteKejelasanPembagianTugasTimCommand>
    {
        public DeleteKejelasanPembagianTugasTimCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
