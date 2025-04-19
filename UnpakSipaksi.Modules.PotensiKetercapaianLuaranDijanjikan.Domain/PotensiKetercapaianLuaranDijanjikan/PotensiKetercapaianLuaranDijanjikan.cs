using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan
{
    public sealed partial class PotensiKetercapaianLuaranDijanjikan : Entity
    {
        private PotensiKetercapaianLuaranDijanjikan()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int BobotPDP { get; private set; } = 0;
        public int BobotTerapan { get; private set; } = 0;
        public int BobotKerjasama { get; private set; } = 0;
        public int BobotPenelitianDasar { get; private set; } = 0;
        public int Skor { get; private set; } = 0;

        public static PotensiKetercapaianLuaranDijanjikanBuilder Update(PotensiKetercapaianLuaranDijanjikan prev) => new PotensiKetercapaianLuaranDijanjikanBuilder(prev);

        public static Result<PotensiKetercapaianLuaranDijanjikan> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new PotensiKetercapaianLuaranDijanjikan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new PotensiKetercapaianLuaranDijanjikanCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
