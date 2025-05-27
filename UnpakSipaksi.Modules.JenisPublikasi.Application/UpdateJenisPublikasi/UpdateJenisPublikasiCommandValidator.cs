using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.UpdateJenisPublikasi
{
    public sealed class UpdateJenisPublikasiCommandValidator : AbstractValidator<UpdateJenisPublikasiCommand>
    {
        public UpdateJenisPublikasiCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Sbu)
                .NotEmpty().WithMessage("'Sbu' tidak boleh kosong.");
        }
    }
}
