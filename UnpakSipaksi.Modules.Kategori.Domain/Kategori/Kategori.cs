using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Kategori.Domain.Kategori
{
    public sealed partial class Kategori : Entity
    {
        private Kategori()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("nama")]
        public string Nama { get; private set; } = null!;
        
        public static KategoriBuilder Update(Kategori prev) => new KategoriBuilder(prev);

        public static Result<Kategori> Create(
        string Nama
        )
        {
            var asset = new Kategori
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new KategoriCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
