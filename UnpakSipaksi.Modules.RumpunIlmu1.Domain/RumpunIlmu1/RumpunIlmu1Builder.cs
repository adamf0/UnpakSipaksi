using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1
{
    public sealed partial class RumpunIlmu1
    {
        public sealed class RumpunIlmu1Builder
        {
            private readonly RumpunIlmu1 _fokusPenelitian;
            private Result? _result;

            public RumpunIlmu1Builder(RumpunIlmu1 fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RumpunIlmu1> Build()
            {
                return HasError ? Result.Failure<RumpunIlmu1>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public RumpunIlmu1Builder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RumpunIlmu1>(RumpunIlmu1Errors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
        }
    }
}
