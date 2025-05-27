using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.UpdateAuthorSinta
{
    public sealed class UpdateAuthorSintaCommandValidator : AbstractValidator<UpdateAuthorSintaCommand>
    {
        public UpdateAuthorSintaCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nidn)
                .NotEmpty().WithMessage("'Nidn' tidak boleh kosong.");

            RuleFor(c => c.AuthorId)
                .NotEmpty().WithMessage("'AuthorId' tidak boleh kosong.");

            RuleFor(c => c.Score)
                .NotEmpty().WithMessage("'Score' tidak boleh kosong.");
        }
    }
}
