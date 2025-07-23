using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Referensi.Domain.Referensi
{
    public sealed partial class Referensi
    {
        public sealed class ReferensiBuilder
        {
            private readonly Referensi _Referensi;
            private Result? _result;

            public ReferensiBuilder(Referensi Referensi)
            {
                _Referensi = Referensi;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Referensi> Build()
            {
                return HasError ? Result.Failure<Referensi>(_result!.Error) : Result.Success(_Referensi);
            }

            public ReferensiBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<Referensi>(ReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _Referensi.Nama = nama;
                return this;
            }

            public ReferensiBuilder ChangeKebaruanReferensiId(int KebaruanReferensiId)
            {
                if (HasError) return this;

                if (KebaruanReferensiId <= 0) {
                    _result = Result.Failure<Referensi>(ReferensiErrors.NotFoundKebaruanReferensiId());
                    return this;
                }

                _Referensi.KebaruanReferensiId = KebaruanReferensiId;
                return this;
            }

            public ReferensiBuilder ChangeRelevansiKualitasReferensiId(int RelevansiKualitasReferensiId)
            {
                if (HasError) return this;

                if (RelevansiKualitasReferensiId <= 0) {
                    _result = Result.Failure<Referensi>(ReferensiErrors.NotFoundRelevansiKualitasReferensiId());
                    return this;
                }

                _Referensi.RelevansiKualitasReferensiId = RelevansiKualitasReferensiId;
                return this;
            }

            public ReferensiBuilder ChangeNilai(int Nilai)
            {
                if (HasError) return this;

                if (Nilai < 0) {
                    _result = Result.Failure<Referensi>(ReferensiErrors.InvalidValueNilai());
                    return this;
                }

                _Referensi.Nilai = Nilai;
                return this;
            }

        }
    }
}
