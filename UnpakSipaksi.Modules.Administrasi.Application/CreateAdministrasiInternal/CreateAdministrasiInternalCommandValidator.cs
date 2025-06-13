using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.CreateAdministrasiInternal
{
    public sealed class CreateAdministrasiInternalCommandValidator : AbstractValidator<CreateAdministrasiInternalCommand>
    {
        public CreateAdministrasiInternalCommandValidator()
        {
            RuleFor(c => c.level)
                .NotEmpty().WithMessage("'Level' tidak boleh kosong.");

            RuleFor(c => c.keputusan)
                .NotEmpty().WithMessage("'Keputusan' tidak boleh kosong.");
        }
    }
}
