using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;

namespace UnpakSipaksi.Modules.KategoriLuaran.Domain.KategoriLuaran
{
    public sealed partial class KategoriLuaran
    {
        public sealed class KategoriLuaranBuilder
        {
            private readonly KategoriLuaran _akurasiPenelitian;
            private Result? _result;

            public KategoriLuaranBuilder(KategoriLuaran akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KategoriLuaran> Build()
            {
                return HasError ? Result.Failure<KategoriLuaran>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriLuaranBuilder ChangeKategoriId(int kategoriId)
            {
                if (HasError) return this;

                if (kategoriId <= 0)
                {
                    _result = Result.Failure<KategoriLuaran>(KategoriLuaranErrors.KategoriNotFound());
                    return this;
                }

                _akurasiPenelitian.KategoriId = kategoriId;
                return this;
            }
            public KategoriLuaranBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriLuaran>(KategoriLuaranErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
            public KategoriLuaranBuilder ChangeStatus(string status)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriLuaran>(KategoriLuaranErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Status = status;
                return this;
            }
        }
    }
}
