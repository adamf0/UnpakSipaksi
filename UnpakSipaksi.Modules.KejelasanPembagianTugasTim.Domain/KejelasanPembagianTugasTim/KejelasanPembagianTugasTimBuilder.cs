using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim
{
    public sealed partial class KejelasanPembagianTugasTim
    {
        public sealed class KejelasanPembagianTugasTimBuilder
        {
            private readonly KejelasanPembagianTugasTim _akurasiPenelitian;
            private Result? _result;

            public KejelasanPembagianTugasTimBuilder(KejelasanPembagianTugasTim akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KejelasanPembagianTugasTim> Build()
            {
                return HasError ? Result.Failure<KejelasanPembagianTugasTim>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KejelasanPembagianTugasTimBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KejelasanPembagianTugasTim>(KejelasanPembagianTugasTimErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KejelasanPembagianTugasTimBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KejelasanPembagianTugasTim>(KejelasanPembagianTugasTimErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }
        }
    }
}
