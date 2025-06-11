using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian
{
    public sealed class CreateIndikatorCapaianCommandValidator : AbstractValidator<CreateIndikatorCapaianCommand>
    {
        public CreateIndikatorCapaianCommandValidator()
        {
            RuleFor(c => c.UuidJenisLuaran)
                .NotEmpty().WithMessage("'JenisLuaran' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'JenisLuaran' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("'Status' tidak boleh kosong.");
        }
    }
}
