using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel
{
    public sealed partial class LuaranArtikel : Entity
    {
        private LuaranArtikel()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static LuaranArtikelBuilder Update(LuaranArtikel prev) => new LuaranArtikelBuilder(prev);

        public static Result<LuaranArtikel> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0) {
                return Result.Failure<LuaranArtikel>(LuaranArtikelErrors.InvalidValueNilai());
            }
            var asset = new LuaranArtikel
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new LuaranArtikelCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
