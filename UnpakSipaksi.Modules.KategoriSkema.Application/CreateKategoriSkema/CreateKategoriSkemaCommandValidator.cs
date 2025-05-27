using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.CreateKategoriSkema
{
    public sealed class CreateKategoriSkemaCommandValidator : AbstractValidator<CreateKategoriSkemaCommand>
    {
        public CreateKategoriSkemaCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Rule)
                .NotEmpty().WithMessage("'Rule' tidak boleh kosong.");
        }
    }
}
