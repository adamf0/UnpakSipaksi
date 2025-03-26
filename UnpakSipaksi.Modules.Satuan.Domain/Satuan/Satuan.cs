using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Satuan.Domain.Satuan
{
    public sealed partial class Satuan : Entity
    {
        private Satuan()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public static SatuanBuilder Update(Satuan prev) => new SatuanBuilder(prev);

        public static Result<Satuan> Create(
        string Nama
        )
        {
            var asset = new Satuan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new SatuanCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
