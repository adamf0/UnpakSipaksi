using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan
{
    public sealed partial class KesesuaianPenugasan : Entity
    {
        private KesesuaianPenugasan()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KesesuaianPenugasanBuilder Update(KesesuaianPenugasan prev) => new KesesuaianPenugasanBuilder(prev);

        public static Result<KesesuaianPenugasan> Create(
        string Nama,
        int Nilai
        )
        {
            var asset = new KesesuaianPenugasan
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KesesuaianPenugasanCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
