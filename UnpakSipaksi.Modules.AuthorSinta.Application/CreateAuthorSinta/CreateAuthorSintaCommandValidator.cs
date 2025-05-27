using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.CreateAuthorSinta
{
    public sealed class CreateAuthorSintaCommandValidator : AbstractValidator<CreateAuthorSintaCommand>
    {
        public CreateAuthorSintaCommandValidator()
        {
            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.AuthorId)
                .NotEmpty().WithMessage("'AuthorId' tidak boleh kosong.");

            RuleFor(c => c.Score)
                .NotEmpty().WithMessage("'Score' tidak boleh kosong.");
        }
    }
}
