using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian
{
    public sealed partial class RoadmapPenelitian : Entity
    {
        private RoadmapPenelitian()
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

        public static RoadmapPenelitianBuilder Update(RoadmapPenelitian prev) => new RoadmapPenelitianBuilder(prev);

        public static Result<RoadmapPenelitian> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new RoadmapPenelitian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new RoadmapPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
