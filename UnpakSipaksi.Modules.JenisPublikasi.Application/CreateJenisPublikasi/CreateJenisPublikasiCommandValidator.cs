using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.CreateJenisPublikasi
{
    public sealed class CreateJenisPublikasiCommandValidator : AbstractValidator<CreateJenisPublikasiCommand>
    {
        public CreateJenisPublikasiCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Sbu)
                .NotEmpty().WithMessage("'Sbu' tidak boleh kosong.");
        }
    }
}
