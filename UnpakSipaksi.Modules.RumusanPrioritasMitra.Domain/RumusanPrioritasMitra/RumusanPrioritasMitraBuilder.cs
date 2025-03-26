using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra
{
    public sealed partial class RumusanPrioritasMitra
    {
        public sealed class RumusanPrioritasMitraBuilder
        {
            private readonly RumusanPrioritasMitra _akurasiPenelitian;
            private Result? _result;

            public RumusanPrioritasMitraBuilder(RumusanPrioritasMitra akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RumusanPrioritasMitra> Build()
            {
                return HasError ? Result.Failure<RumusanPrioritasMitra>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public RumusanPrioritasMitraBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RumusanPrioritasMitra>(RumusanPrioritasMitraErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public RumusanPrioritasMitraBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RumusanPrioritasMitra>(RumusanPrioritasMitraErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
