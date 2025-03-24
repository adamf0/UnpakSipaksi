using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Rirn.Domain.Rirn
{
    public sealed partial class Rirn
    {
        public sealed class RirnBuilder
        {
            private readonly Rirn _fokusPenelitian;
            private Result? _result;

            public RirnBuilder(Rirn fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Rirn> Build()
            {
                return HasError ? Result.Failure<Rirn>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public RirnBuilder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<Rirn>(RirnErrors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
        }
    }
}
