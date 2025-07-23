using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using System.Text.Json;

namespace UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema
{
    public sealed partial class KategoriSkema : Entity
    {
        private KategoriSkema()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public string Rule { get; private set; }

        public static KategoriSkemaBuilder Update(KategoriSkema prev) => new KategoriSkemaBuilder(prev);

        public static Result<KategoriSkema> Create(
        string Nama,
        string Rule
        )
        {
            try
            {
                using var doc = JsonDocument.Parse(Rule);
                if (doc.RootElement.ValueKind != JsonValueKind.Array)
                {
                    return Result.Failure<KategoriSkema>(KategoriSkemaErrors.InvalidFormatRule());
                }
            }
            catch (JsonException)
            {
                return Result.Failure<KategoriSkema>(KategoriSkemaErrors.InvalidFormatRule());
            }

            var asset = new KategoriSkema
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Rule = Rule
            };

            asset.Raise(new KategoriSkemaCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
