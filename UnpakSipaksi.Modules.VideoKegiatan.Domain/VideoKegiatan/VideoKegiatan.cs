using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan
{
    public sealed partial class VideoKegiatan : Entity
    {
        private VideoKegiatan()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; } = 0;

        public static VideoKegiatanBuilder Update(VideoKegiatan prev) => new VideoKegiatanBuilder(prev);

        public static Result<VideoKegiatan> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new VideoKegiatan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new VideoKegiatanCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
