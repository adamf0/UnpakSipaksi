using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset
{
    public sealed partial class PrioritasRiset
    {
        public sealed class PrioritasRisetBuilder
        {
            private readonly PrioritasRiset _fokusPenelitian;
            private Result? _result;

            public PrioritasRisetBuilder(PrioritasRiset fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<PrioritasRiset> Build()
            {
                return HasError ? Result.Failure<PrioritasRiset>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public PrioritasRisetBuilder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PrioritasRiset>(PrioritasRisetErrors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
        }
    }
}
