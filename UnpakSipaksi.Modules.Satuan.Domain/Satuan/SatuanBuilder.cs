using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Satuan.Domain.Satuan
{
    public sealed partial class Satuan
    {
        public sealed class SatuanBuilder
        {
            private readonly Satuan _fokusPenelitian;
            private Result? _result;

            public SatuanBuilder(Satuan fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Satuan> Build()
            {
                return HasError ? Result.Failure<Satuan>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public SatuanBuilder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<Satuan>(SatuanErrors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
        }
    }
}
