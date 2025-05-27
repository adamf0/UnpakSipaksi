using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian
{
    public sealed class CreateFokusPengabdianCommandValidator : AbstractValidator<CreateFokusPengabdianCommand>
    {
        public CreateFokusPengabdianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
