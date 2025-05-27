using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.CreateKategoriSumberDana
{
    public sealed class CreateKategoriSumberDanaCommandValidator : AbstractValidator<CreateKategoriSumberDanaCommand>
    {
        public CreateKategoriSumberDanaCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
