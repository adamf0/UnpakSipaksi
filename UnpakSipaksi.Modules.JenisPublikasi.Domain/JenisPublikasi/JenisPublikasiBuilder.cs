using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi
{
    public sealed partial class JenisPublikasi
    {
        public sealed class JenisPublikasiBuilder
        {
            private readonly JenisPublikasi _akurasiPenelitian;
            private Result? _result;

            public JenisPublikasiBuilder(JenisPublikasi akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<JenisPublikasi> Build()
            {
                return HasError ? Result.Failure<JenisPublikasi>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public JenisPublikasiBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<JenisPublikasi>(JenisPublikasiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public JenisPublikasiBuilder ChangeSbu(int sbu)
            {
                if (HasError) return this;

                if (sbu < 0)
                {
                    _result = Result.Failure<JenisPublikasi>(JenisPublikasiErrors.InvalidSbu());
                    return this;
                }

                _akurasiPenelitian.Sbu = sbu;
                return this;
            }
        }
    }
}
