using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian
{
    public sealed partial class KategoriMitraPenelitian
    {
        public sealed class KategoriMitraPenelitianBuilder
        {
            private readonly KategoriMitraPenelitian _akurasiPenelitian;
            private Result? _result;

            public KategoriMitraPenelitianBuilder(KategoriMitraPenelitian akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KategoriMitraPenelitian> Build()
            {
                return HasError ? Result.Failure<KategoriMitraPenelitian>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriMitraPenelitianBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriMitraPenelitian>(KategoriMitraPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
        }
    }
}
