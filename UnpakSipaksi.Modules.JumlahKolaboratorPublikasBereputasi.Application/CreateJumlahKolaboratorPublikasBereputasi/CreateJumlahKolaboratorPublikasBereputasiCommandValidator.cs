using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.CreateJumlahKolaboratorPublikasBereputasi
{
    public sealed class CreateJumlahKolaboratorPublikasBereputasiCommandValidator : AbstractValidator<CreateJumlahKolaboratorPublikasBereputasiCommand>
    {
        public CreateJumlahKolaboratorPublikasBereputasiCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.");
        }
    }
}
