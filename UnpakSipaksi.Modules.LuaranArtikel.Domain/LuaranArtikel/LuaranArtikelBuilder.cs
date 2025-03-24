using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel
{
    public sealed partial class LuaranArtikel
    {
        public sealed class LuaranArtikelBuilder
        {
            private readonly LuaranArtikel _akurasiPenelitian;
            private Result? _result;

            public LuaranArtikelBuilder(LuaranArtikel akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<LuaranArtikel> Build()
            {
                return HasError ? Result.Failure<LuaranArtikel>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public LuaranArtikelBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<LuaranArtikel>(LuaranArtikelErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public LuaranArtikelBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<LuaranArtikel>(LuaranArtikelErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
