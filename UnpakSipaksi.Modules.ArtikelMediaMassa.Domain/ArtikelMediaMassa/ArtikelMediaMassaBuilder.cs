using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa
{
    public sealed partial class ArtikelMediaMassa
    {
        public sealed class ArtikelMediaMassaBuilder
        {
            private readonly ArtikelMediaMassa _akurasiPenelitian;
            private Result? _result;

            public ArtikelMediaMassaBuilder(ArtikelMediaMassa akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<ArtikelMediaMassa> Build()
            {
                return HasError ? Result.Failure<ArtikelMediaMassa>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public ArtikelMediaMassaBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ArtikelMediaMassa>(ArtikelMediaMassaErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public ArtikelMediaMassaBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ArtikelMediaMassa>(ArtikelMediaMassaErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
