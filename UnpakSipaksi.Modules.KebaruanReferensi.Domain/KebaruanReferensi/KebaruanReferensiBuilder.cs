using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi
{
    public sealed partial class KebaruanReferensi
    {
        public sealed class KebaruanReferensiBuilder
        {
            private readonly KebaruanReferensi _akurasiPenelitian;
            private Result? _result;

            public KebaruanReferensiBuilder(KebaruanReferensi akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KebaruanReferensi> Build()
            {
                return HasError ? Result.Failure<KebaruanReferensi>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KebaruanReferensiBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KebaruanReferensi>(KebaruanReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KebaruanReferensiBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KebaruanReferensi>(KebaruanReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }
        }
    }
}
