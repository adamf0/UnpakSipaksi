using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.UpdateFokusPengabdian
{
    public sealed class UpdateFokusPengabdianCommandValidator : AbstractValidator<CreateFokusPengabdianCommand>
    {
        public UpdateFokusPengabdianCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
