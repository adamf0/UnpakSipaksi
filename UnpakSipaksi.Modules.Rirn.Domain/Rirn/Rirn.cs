using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Rirn.Domain.Rirn
{
    public sealed partial class Rirn : Entity
    {
        private Rirn()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public static RirnBuilder Update(Rirn prev) => new RirnBuilder(prev);

        public static Result<Rirn> Create(
        string Nama
        )
        {
            var asset = new Rirn
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new RirnCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
