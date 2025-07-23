using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas
{
    public sealed partial class KesesuaianWaktuRabLuaranFasilitas : Entity
    {
        private KesesuaianWaktuRabLuaranFasilitas()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Skor { get; private set; }

        public static KesesuaianWaktuRabLuaranFasilitasBuilder Update(KesesuaianWaktuRabLuaranFasilitas prev) => new KesesuaianWaktuRabLuaranFasilitasBuilder(prev);

        public static Result<KesesuaianWaktuRabLuaranFasilitas> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor < 0)
            {
                return Result.Failure<KesesuaianWaktuRabLuaranFasilitas>(KesesuaianWaktuRabLuaranFasilitasErrors.InvalidValueSkor());
            }
            var asset = new KesesuaianWaktuRabLuaranFasilitas
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor
            };

            asset.Raise(new KesesuaianWaktuRabLuaranFasilitasCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
