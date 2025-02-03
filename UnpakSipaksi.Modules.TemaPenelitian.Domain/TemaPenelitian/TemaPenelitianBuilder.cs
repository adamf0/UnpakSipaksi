using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian
{
    public sealed partial class TemaPenelitian
    {
        public sealed class TemaPenelitianBuilder
        {
            private readonly TemaPenelitian _temaPenelitian;
            private Result? _result;

            public TemaPenelitianBuilder(TemaPenelitian fokusPenelitian)
            {
                _temaPenelitian = fokusPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<TemaPenelitian> Build()
            {
                return HasError ? Result.Failure<TemaPenelitian>(_result!.Error) : Result.Success(_temaPenelitian);
            }

            public TemaPenelitianBuilder ChangeNama(string Nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<TemaPenelitian>(TemaPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _temaPenelitian.Nama = Nama;
                return this;
            }
            public TemaPenelitianBuilder ChangeTemaPenelitianId(int FokusPenelitianId)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<TemaPenelitian>(TemaPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _temaPenelitian.FokusPenelitianId = FokusPenelitianId;
                return this;
            }
        }
    }
}
