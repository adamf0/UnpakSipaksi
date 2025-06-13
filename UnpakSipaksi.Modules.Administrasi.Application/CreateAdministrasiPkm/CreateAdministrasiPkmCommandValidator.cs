using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.CreateAdministrasiPkm
{
    public sealed class CreateAdministrasiPkmCommandValidator : AbstractValidator<CreateAdministrasiPkmCommand>
    {
        public CreateAdministrasiPkmCommandValidator()
        {
            RuleFor(c => c.level)
                .NotEmpty().WithMessage("'Level' tidak boleh kosong.");

            RuleFor(c => c.keputusan)
                .NotEmpty().WithMessage("'Keputusan' tidak boleh kosong.");
        }
    }
}
