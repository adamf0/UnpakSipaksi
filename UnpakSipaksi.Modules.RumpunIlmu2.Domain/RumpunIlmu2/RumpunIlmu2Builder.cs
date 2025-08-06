using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2
{
    public sealed partial class RumpunIlmu2
    {
        public sealed class RumpunIlmu2Builder
        {
            private readonly RumpunIlmu2 _fokusPenelitian;
            private Result? _result;

            public RumpunIlmu2Builder(RumpunIlmu2 fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RumpunIlmu2> Build()
            {
                return HasError ? Result.Failure<RumpunIlmu2>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public RumpunIlmu2Builder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RumpunIlmu2>(RumpunIlmu2Errors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
            public RumpunIlmu2Builder ChangeIdRumpunIlmu1(int IdRumpunIlmu1)
            {
                if (HasError) return this;

                if (IdRumpunIlmu1 <= 0)
                {
                    _result = Result.Failure<RumpunIlmu2>(RumpunIlmu2Errors.UnknownRumpunIlmu1());
                    return this;
                }

                _fokusPenelitian.IdRumpunIlmu1 = IdRumpunIlmu1;
                return this;
            }
        }
    }
}
