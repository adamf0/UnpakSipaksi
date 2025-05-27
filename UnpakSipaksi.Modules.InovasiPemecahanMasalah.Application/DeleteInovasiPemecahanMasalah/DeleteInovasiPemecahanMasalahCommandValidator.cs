using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.DeleteInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.DeleteInovasiPemecahanMasalah
{
    public sealed class DeleteInovasiPemecahanMasalahCommandValidator : AbstractValidator<DeleteInovasiPemecahanMasalahCommand>
    {
        public DeleteInovasiPemecahanMasalahCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
