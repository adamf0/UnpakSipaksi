using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian
{
    public sealed partial class KategoriProgramPengabdian : Entity
    {
        private KategoriProgramPengabdian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public string Rule { get; private set; }

        public static KategoriProgramPengabdianBuilder Update(KategoriProgramPengabdian prev) => new KategoriProgramPengabdianBuilder(prev);

        public static Result<KategoriProgramPengabdian> Create(
        string Nama,
        string Rule
        )
        {
            var asset = new KategoriProgramPengabdian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Rule = Rule
            };

            asset.Raise(new KategoriProgramPengabdianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
