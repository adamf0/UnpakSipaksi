using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra
{
    public sealed partial class KelompokMitra
    {
        public sealed class KelompokMitraBuilder
        {
            private readonly KelompokMitra _akurasiPenelitian;
            private Result? _result;

            public KelompokMitraBuilder(KelompokMitra akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KelompokMitra> Build()
            {
                return HasError ? Result.Failure<KelompokMitra>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KelompokMitraBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KelompokMitra>(KelompokMitraErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }
        }
    }
}
