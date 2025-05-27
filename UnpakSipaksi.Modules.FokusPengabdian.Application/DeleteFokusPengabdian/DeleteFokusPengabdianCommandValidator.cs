using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.DeleteFokusPengabdian
{
    public sealed class DeleteFokusPengabdianCommandValidator : AbstractValidator<DeleteFokusPengabdianCommand>
    {
        public DeleteFokusPengabdianCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
