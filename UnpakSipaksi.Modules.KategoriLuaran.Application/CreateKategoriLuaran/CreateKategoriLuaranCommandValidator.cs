using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.CreateKategoriLuaran
{
    public sealed class CreateKategoriLuaranCommandValidator : AbstractValidator<CreateKategoriLuaranCommand>
    {
        public CreateKategoriLuaranCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
