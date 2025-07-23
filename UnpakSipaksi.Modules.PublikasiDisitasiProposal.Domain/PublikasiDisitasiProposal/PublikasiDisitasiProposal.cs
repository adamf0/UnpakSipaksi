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
        public int Skor { get; private set; } = 0;

        public static PublikasiDisitasiProposalBuilder Update(PublikasiDisitasiProposal prev) => new PublikasiDisitasiProposalBuilder(prev);

        public static Result<PublikasiDisitasiProposal> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor < 0) {
                return Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.InvalidValueSkor());
            }
            var asset = new PublikasiDisitasiProposal
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new PublikasiDisitasiProposalCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
