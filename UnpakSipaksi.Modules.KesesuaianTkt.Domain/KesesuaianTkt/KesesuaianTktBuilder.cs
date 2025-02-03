using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt
{
    public sealed partial class KesesuaianTkt
    {
        public sealed class KesesuaianTktBuilder
        {
            private readonly KesesuaianTkt _akurasiPenelitian;
            private Result? _result;

            public KesesuaianTktBuilder(KesesuaianTkt akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KesesuaianTkt> Build()
            {
                return HasError ? Result.Failure<KesesuaianTkt>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KesesuaianTktBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KesesuaianTkt>(KesesuaianTktErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KesesuaianTktBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KesesuaianTkt>(KesesuaianTktErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }
        }
    }
}
