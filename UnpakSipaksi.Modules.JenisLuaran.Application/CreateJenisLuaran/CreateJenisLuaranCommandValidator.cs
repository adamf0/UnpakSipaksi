using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.CreateJenisLuaran
{
    public sealed class CreateJenisLuaranCommandValidator : AbstractValidator<CreateJenisLuaranCommand>
    {
        public CreateJenisLuaranCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
