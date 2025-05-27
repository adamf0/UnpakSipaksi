using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal
{
    public sealed partial class RelevansiKepakaranTemaProposal : Entity
    {
        private RelevansiKepakaranTemaProposal()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int Skor { get; private set; } = 0;

        public static RelevansiKepakaranTemaProposalBuilder Update(RelevansiKepakaranTemaProposal prev) => new RelevansiKepakaranTemaProposalBuilder(prev);

        public static Result<RelevansiKepakaranTemaProposal> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new RelevansiKepakaranTemaProposal
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new RelevansiKepakaranTemaProposalCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
