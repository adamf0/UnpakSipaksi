using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas
{
    public sealed partial class KesesuaianWaktuRabLuaranFasilitas
    {
        public sealed class KesesuaianWaktuRabLuaranFasilitasBuilder
        {
            private readonly KesesuaianWaktuRabLuaranFasilitas _akurasiPenelitian;
            private Result? _result;

            public KesesuaianWaktuRabLuaranFasilitasBuilder(KesesuaianWaktuRabLuaranFasilitas akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KesesuaianWaktuRabLuaranFasilitas> Build()
            {
                return HasError ? Result.Failure<KesesuaianWaktuRabLuaranFasilitas>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KesesuaianWaktuRabLuaranFasilitasBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KesesuaianWaktuRabLuaranFasilitas>(KesesuaianWaktuRabLuaranFasilitasErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KesesuaianWaktuRabLuaranFasilitasBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                if (skor < 0)
                {
                    _result = Result.Failure<KesesuaianWaktuRabLuaranFasilitas>(KesesuaianWaktuRabLuaranFasilitasErrors.InvalidValueSkor());
                    return this;
                }

                _akurasiPenelitian.Skor = skor;
                return this;
            }
        }
    }
}
