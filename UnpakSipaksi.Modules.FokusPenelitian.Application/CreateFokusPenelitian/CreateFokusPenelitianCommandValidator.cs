using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.CreateFokusPenelitian
{
    public sealed class CreateFokusPenelitianCommandValidator : AbstractValidator<CreateFokusPenelitianCommand>
    {
        public CreateFokusPenelitianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
