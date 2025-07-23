using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan
{
    public sealed partial class KesesuaianPenugasan
    {
        public sealed class KesesuaianPenugasanBuilder
        {
            private readonly KesesuaianPenugasan _akurasiPenelitian;
            private Result? _result;

            public KesesuaianPenugasanBuilder(KesesuaianPenugasan akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KesesuaianPenugasan> Build()
            {
                return HasError ? Result.Failure<KesesuaianPenugasan>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KesesuaianPenugasanBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KesesuaianPenugasan>(KesesuaianPenugasanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KesesuaianPenugasanBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                if (nilai < 0)
                {
                    _result = Result.Failure<KesesuaianPenugasan>(KesesuaianPenugasanErrors.InvalidValueNilai());
                    return this;
                }

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
