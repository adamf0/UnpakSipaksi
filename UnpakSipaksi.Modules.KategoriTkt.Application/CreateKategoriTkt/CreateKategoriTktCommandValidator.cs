using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.CreateKategoriTkt
{
    public sealed class CreateKategoriTktCommandValidator : AbstractValidator<CreateKategoriTktCommand>
    {
        public CreateKategoriTktCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
