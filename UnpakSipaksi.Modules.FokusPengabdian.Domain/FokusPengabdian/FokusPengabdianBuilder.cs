using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian
{
    public sealed partial class FokusPengabdian
    {
        public sealed class FokusPengabdianBuilder
        {
            private readonly FokusPengabdian _akurasiPenelitian;
            private Result? _result;

            public FokusPengabdianBuilder(FokusPengabdian akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<FokusPengabdian> Build()
            {
                return HasError ? Result.Failure<FokusPengabdian>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public FokusPengabdianBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<FokusPengabdian>(FokusPengabdianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
        }
    }
}
