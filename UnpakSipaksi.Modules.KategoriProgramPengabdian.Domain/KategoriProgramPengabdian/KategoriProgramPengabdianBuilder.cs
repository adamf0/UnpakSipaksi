using System.Text.Json;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian
{
    public sealed partial class KategoriProgramPengabdian
    {
        public sealed class KategoriProgramPengabdianBuilder
        {
            private readonly KategoriProgramPengabdian _akurasiPenelitian;
            private Result? _result;

            public KategoriProgramPengabdianBuilder(KategoriProgramPengabdian akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KategoriProgramPengabdian> Build()
            {
                return HasError ? Result.Failure<KategoriProgramPengabdian>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriProgramPengabdianBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriProgramPengabdian>(KategoriProgramPengabdianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KategoriProgramPengabdianBuilder ChangeRule(string rule)
            {
                if (HasError) return this;

                try
                {
                    using var doc = JsonDocument.Parse(rule);
                    if (doc.RootElement.ValueKind != JsonValueKind.Array)
                    {
                        _result = Result.Failure<KategoriProgramPengabdian>(KategoriProgramPengabdianErrors.InvalidFormatRule());
                        return this;
                    }
                }
                catch (JsonException)
                {
                    _result = Result.Failure<KategoriProgramPengabdian>(KategoriProgramPengabdianErrors.InvalidFormatRule());
                    return this;
                }
                
                _akurasiPenelitian.Rule = rule;
                return this;
            }

        }
    }
}
