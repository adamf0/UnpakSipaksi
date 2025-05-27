using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian
{
    public sealed class DeleteAkurasiPenelitianCommandValidator : AbstractValidator<DeleteAkurasiPenelitianCommand>
    {
        public DeleteAkurasiPenelitianCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
