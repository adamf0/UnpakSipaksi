using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Roadmap.Domain.Roadmap
{
    public sealed partial class Roadmap : Entity
    {
        private Roadmap()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nidn { get; private set; }
        public string Link { get; private set; }

        public static RoadmapBuilder Update(Roadmap prev) => new RoadmapBuilder(prev);

        public static Result<Roadmap> Create(
        string Nidn,
        string Link
        )
        {
            var asset = new Roadmap
            {
                Uuid = Guid.NewGuid(),
                Nidn = Nidn,
                Link = Link
            };

            asset.Raise(new RoadmapCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
