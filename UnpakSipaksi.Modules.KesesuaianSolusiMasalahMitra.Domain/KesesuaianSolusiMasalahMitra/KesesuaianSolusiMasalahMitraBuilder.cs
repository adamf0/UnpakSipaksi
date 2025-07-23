using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra
{
    public sealed partial class KesesuaianSolusiMasalahMitra
    {
        public sealed class KesesuaianSolusiMasalahMitraBuilder
        {
            private readonly KesesuaianSolusiMasalahMitra _akurasiPenelitian;
            private Result? _result;

            public KesesuaianSolusiMasalahMitraBuilder(KesesuaianSolusiMasalahMitra akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KesesuaianSolusiMasalahMitra> Build()
            {
                return HasError ? Result.Failure<KesesuaianSolusiMasalahMitra>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KesesuaianSolusiMasalahMitraBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KesesuaianSolusiMasalahMitra>(KesesuaianSolusiMasalahMitraErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KesesuaianSolusiMasalahMitraBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                if (nilai < 0)
                {
                    _result = Result.Failure<KesesuaianSolusiMasalahMitra>(KesesuaianSolusiMasalahMitraErrors.InvalidValueNilai());
                    return this;
                }

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
