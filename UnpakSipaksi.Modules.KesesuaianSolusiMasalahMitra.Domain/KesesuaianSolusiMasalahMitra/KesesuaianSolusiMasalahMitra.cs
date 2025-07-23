using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra
{
    public sealed partial class KesesuaianSolusiMasalahMitra : Entity
    {
        private KesesuaianSolusiMasalahMitra()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; }

        public static KesesuaianSolusiMasalahMitraBuilder Update(KesesuaianSolusiMasalahMitra prev) => new KesesuaianSolusiMasalahMitraBuilder(prev);

        public static Result<KesesuaianSolusiMasalahMitra> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0) {
                return Result.Failure<KesesuaianSolusiMasalahMitra>(KesesuaianSolusiMasalahMitraErrors.InvalidValueNilai());
            }
            var asset = new KesesuaianSolusiMasalahMitra
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai
            };

            asset.Raise(new KesesuaianSolusiMasalahMitraCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
