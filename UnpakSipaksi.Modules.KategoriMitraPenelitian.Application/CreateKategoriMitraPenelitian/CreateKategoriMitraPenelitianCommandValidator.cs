using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.CreateKategoriMitraPenelitian
{
    public sealed class CreateKategoriMitraPenelitianCommandValidator : AbstractValidator<CreateKategoriMitraPenelitianCommand>
    {
        public CreateKategoriMitraPenelitianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
