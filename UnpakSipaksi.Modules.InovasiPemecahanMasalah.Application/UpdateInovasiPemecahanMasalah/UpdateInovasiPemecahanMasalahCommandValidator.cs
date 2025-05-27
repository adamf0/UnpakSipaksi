using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.UpdateInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.UpdateInovasiPemecahanMasalah
{
    public sealed class UpdateInovasiPemecahanMasalahCommandValidator : AbstractValidator<UpdateInovasiPemecahanMasalahCommand>
    {
        public UpdateInovasiPemecahanMasalahCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.");
        }
    }
}
