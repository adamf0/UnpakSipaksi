using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab
{
    public sealed partial class KelompokRab : Entity
    {
        private KelompokRab()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        
        public static KelompokRabBuilder Update(KelompokRab prev) => new KelompokRabBuilder(prev);

        public static Result<KelompokRab> Create(
        string Nama
        )
        {
            var asset = new KelompokRab
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new KelompokRabCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
