using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt
{
    public sealed partial class KategoriTkt : Entity
    {
        private KategoriTkt()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        
        public static KategoriTktBuilder Update(KategoriTkt prev) => new KategoriTktBuilder(prev);

        public static Result<KategoriTkt> Create(
        string Nama
        )
        {
            var asset = new KategoriTkt
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new KategoriTktCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
