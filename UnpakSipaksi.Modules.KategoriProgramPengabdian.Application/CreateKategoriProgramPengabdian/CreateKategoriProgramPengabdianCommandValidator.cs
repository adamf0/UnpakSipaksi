using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.CreateKategoriProgramPengabdian
{
    public sealed class CreateKategoriProgramPengabdianCommandValidator : AbstractValidator<CreateKategoriProgramPengabdianCommand>
    {
        public CreateKategoriProgramPengabdianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Rule)
                .NotEmpty().WithMessage("'Rule' tidak boleh kosong.");
        }
    }
}
