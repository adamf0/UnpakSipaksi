using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt
{
    public sealed partial class KategoriTkt
    {
        public sealed class KategoriTktBuilder
        {
            private readonly KategoriTkt _akurasiPenelitian;
            private Result? _result;

            public KategoriTktBuilder(KategoriTkt akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KategoriTkt> Build()
            {
                return HasError ? Result.Failure<KategoriTkt>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriTktBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriTkt>(KategoriTktErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
        }
    }
}
