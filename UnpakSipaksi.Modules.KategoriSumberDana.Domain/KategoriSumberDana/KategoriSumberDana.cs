using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana
{
    public sealed partial class KategoriSumberDana : Entity
    {
        private KategoriSumberDana()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        
        public static KategoriSumberDanaBuilder Update(KategoriSumberDana prev) => new KategoriSumberDanaBuilder(prev);

        public static Result<KategoriSumberDana> Create(
        string Nama
        )
        {
            var asset = new KategoriSumberDana
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new KategoriSumberDanaCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
