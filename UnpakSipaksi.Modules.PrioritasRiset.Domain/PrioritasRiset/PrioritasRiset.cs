using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset
{
    public sealed partial class PrioritasRiset : Entity
    {
        private PrioritasRiset()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public static PrioritasRisetBuilder Update(PrioritasRiset prev) => new PrioritasRisetBuilder(prev);

        public static Result<PrioritasRiset> Create(
        string Nama
        )
        {
            var asset = new PrioritasRiset
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama
            };

            asset.Raise(new PrioritasRisetCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
