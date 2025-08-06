using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3
{
    public sealed partial class RumpunIlmu3
    {
        public sealed class RumpunIlmu3Builder
        {
            private readonly RumpunIlmu3 _fokusPenelitian;
            private Result? _result;

            public RumpunIlmu3Builder(RumpunIlmu3 fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RumpunIlmu3> Build()
            {
                return HasError ? Result.Failure<RumpunIlmu3>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public RumpunIlmu3Builder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RumpunIlmu3>(RumpunIlmu3Errors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
            public RumpunIlmu3Builder ChangeIdRumpunIlmu2(int IdRumpunIlmu2)
            {
                if (HasError) return this;

                if (IdRumpunIlmu2 <= 0)
                {
                    _result = Result.Failure<RumpunIlmu3>(RumpunIlmu3Errors.UnknownRumpunIlmu2());
                    return this;
                }

                _fokusPenelitian.IdRumpunIlmu2 = IdRumpunIlmu2;
                return this;
            }
        }
    }
}
