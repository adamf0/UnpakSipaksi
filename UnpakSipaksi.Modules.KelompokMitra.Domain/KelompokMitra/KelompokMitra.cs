using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra
{
    public sealed partial class KelompokMitra : Entity
    {
        private KelompokMitra()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        
        public static KelompokMitraBuilder Update(KelompokMitra prev) => new KelompokMitraBuilder(prev);

        public static Result<KelompokMitra> Create(
        string Nama
        )
        {
            var asset = new KelompokMitra
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new KelompokMitraCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
