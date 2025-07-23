using System.Text.Json;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema
{
    public sealed partial class KategoriSkema
    {
        public sealed class KategoriSkemaBuilder
        {
            private readonly KategoriSkema _akurasiPenelitian;
            private Result? _result;

            public KategoriSkemaBuilder(KategoriSkema akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KategoriSkema> Build()
            {
                return HasError ? Result.Failure<KategoriSkema>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KategoriSkemaBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KategoriSkema>(KategoriSkemaErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KategoriSkemaBuilder ChangeRule(string rule)
            {
                if (HasError) return this;

                try
                {
                    using var doc = JsonDocument.Parse(rule);
                    if (doc.RootElement.ValueKind != JsonValueKind.Array)
                    {
                        _result = Result.Failure<KategoriSkema>(KategoriSkemaErrors.InvalidFormatRule());
                        return this;
                    }
                }
                catch (JsonException)
                {
                    _result = Result.Failure<KategoriSkema>(KategoriSkemaErrors.InvalidFormatRule());
                    return this;
                }

                _akurasiPenelitian.Rule = rule;
                return this;
            }

        }
    }
}
