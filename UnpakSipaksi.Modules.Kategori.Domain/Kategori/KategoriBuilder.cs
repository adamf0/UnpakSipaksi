using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Kategori.Domain.Kategori
{
    public sealed partial class Kategori
    {
        public sealed class KategoriBuilder
        {
            private readonly Kategori _akurasiPenelitian;
            private Result? _result;

            public KategoriBuilder(Kategori akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Kategori> Build()
            {
                return HasError ? Result.Failure<Kategori>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<Kategori>(KategoriErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
        }
    }
}
