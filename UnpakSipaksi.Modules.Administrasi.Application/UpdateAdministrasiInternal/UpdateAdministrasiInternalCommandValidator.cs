using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.UpdateAdministrasiInternal
{
    public sealed class UpdateAdministrasiInternalCommandValidator : AbstractValidator<UpdateAdministrasiInternalCommand>
    {
        public UpdateAdministrasiInternalCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.level)
                 .NotEmpty().WithMessage("'Level' tidak boleh kosong.");

            RuleFor(c => c.keputusan)
                .NotEmpty().WithMessage("'Keputusan' tidak boleh kosong.");
        }
    }
}
