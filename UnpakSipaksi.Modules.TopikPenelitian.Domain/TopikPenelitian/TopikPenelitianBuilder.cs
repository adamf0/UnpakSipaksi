using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian
{
    public sealed partial class TopikPenelitian
    {
        public sealed class TopikPenelitianBuilder
        {
            private readonly TopikPenelitian _fokusPenelitian;
            private Result? _result;

            public TopikPenelitianBuilder(TopikPenelitian fokusPenelitian)
            {
                _fokusPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<TopikPenelitian> Build()
            {
                return HasError ? Result.Failure<TopikPenelitian>(_result!.Error) : Result.Success(_fokusPenelitian);
            }

            public TopikPenelitianBuilder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<TopikPenelitian>(TopikPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.Nama = Nama;
                return this;
            }
            public TopikPenelitianBuilder ChangeTemaPenelitianId(int TemaPenelitianId)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<TopikPenelitian>(TopikPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _fokusPenelitian.TemaPenelitianId = TemaPenelitianId;
                return this;
            }
        }
    }
}
