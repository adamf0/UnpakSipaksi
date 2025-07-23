using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal
{
    public sealed partial class KesesuaianJadwal
    {
        public sealed class KesesuaianJadwalBuilder
        {
            private readonly KesesuaianJadwal _akurasiPenelitian;
            private Result? _result;

            public KesesuaianJadwalBuilder(KesesuaianJadwal akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KesesuaianJadwal> Build()
            {
                return HasError ? Result.Failure<KesesuaianJadwal>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KesesuaianJadwalBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KesesuaianJadwal>(KesesuaianJadwalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KesesuaianJadwalBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                if (nilai < 0)
                {
                    _result = Result.Failure<KesesuaianJadwal>(KesesuaianJadwalErrors.InvalidValueNilai());
                    return this;
                }

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
