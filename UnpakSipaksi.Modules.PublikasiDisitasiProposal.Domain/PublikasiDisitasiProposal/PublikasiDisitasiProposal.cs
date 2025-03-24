using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal
{
    public sealed partial class PublikasiDisitasiProposal : Entity
    {
        private PublikasiDisitasiProposal()
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

        public static PublikasiDisitasiProposalBuilder Update(PublikasiDisitasiProposal prev) => new PublikasiDisitasiProposalBuilder(prev);

        public static Result<PublikasiDisitasiProposal> Create(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
        )
        {
            var asset = new PublikasiDisitasiProposal
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                BobotPDP = BobotPDP,
                BobotTerapan = BobotTerapan,
                BobotKerjasama = BobotKerjasama,
                BobotPenelitianDasar = BobotPenelitianDasar,
                Skor = Skor,
            };

            asset.Raise(new PublikasiDisitasiProposalCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
