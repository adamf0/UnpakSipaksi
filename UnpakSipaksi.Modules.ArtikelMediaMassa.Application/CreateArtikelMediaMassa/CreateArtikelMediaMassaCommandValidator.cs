using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.CreateArtikelMediaMassa
{
    public sealed class CreateArtikelMediaMassaCommandValidator : AbstractValidator<CreateArtikelMediaMassaCommand>
    {
        public CreateArtikelMediaMassaCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.");
        }
    }
}
