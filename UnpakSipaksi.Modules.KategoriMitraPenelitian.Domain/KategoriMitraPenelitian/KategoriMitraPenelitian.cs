using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian
{
    public sealed partial class KategoriMitraPenelitian : Entity
    {
        private KategoriMitraPenelitian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        
        public static KategoriMitraPenelitianBuilder Update(KategoriMitraPenelitian prev) => new KategoriMitraPenelitianBuilder(prev);

        public static Result<KategoriMitraPenelitian> Create(
        string Nama
        )
        {
            var asset = new KategoriMitraPenelitian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new KategoriMitraPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
