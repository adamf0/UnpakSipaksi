using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal
{
    public sealed partial class KesesuaianJadwal : Entity
    {
        private KesesuaianJadwal()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KesesuaianJadwalBuilder Update(KesesuaianJadwal prev) => new KesesuaianJadwalBuilder(prev);

        public static Result<KesesuaianJadwal> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new KesesuaianJadwal
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KesesuaianJadwalCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
