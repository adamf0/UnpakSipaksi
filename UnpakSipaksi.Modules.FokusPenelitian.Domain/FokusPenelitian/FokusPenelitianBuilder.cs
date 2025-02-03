using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian
{
    public sealed partial class FokusPenelitian
    {
        public sealed class FokusPenelitianBuilder
        {
            private readonly FokusPenelitian _fokusPenelitian;
            private Result? _result;

            public FokusPenelitianBuilder(FokusPenelitian fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<FokusPenelitian> Build()
            {
                return HasError ? Result.Failure<FokusPenelitian>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public FokusPenelitianBuilder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<FokusPenelitian>(FokusPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }

        }
    }
}
