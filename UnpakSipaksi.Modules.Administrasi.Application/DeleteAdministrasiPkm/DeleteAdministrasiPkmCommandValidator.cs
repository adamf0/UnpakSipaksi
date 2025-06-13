using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.DeleteAdministrasiPkm
{
    public sealed class DeleteAdministrasiPkmCommandValidator : AbstractValidator<DeleteAdministrasiPkmCommand>
    {
        public DeleteAdministrasiPkmCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
