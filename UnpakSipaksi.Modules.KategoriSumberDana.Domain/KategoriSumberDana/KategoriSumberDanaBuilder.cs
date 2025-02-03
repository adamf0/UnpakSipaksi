using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana
{
    public sealed partial class KategoriSumberDana
    {
        public sealed class KategoriSumberDanaBuilder
        {
            private readonly KategoriSumberDana _akurasiPenelitian;
            private Result? _result;

            public KategoriSumberDanaBuilder(KategoriSumberDana akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KategoriSumberDana> Build()
            {
                return HasError ? Result.Failure<KategoriSumberDana>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriSumberDanaBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriSumberDana>(KategoriSumberDanaErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
        }
    }
}
