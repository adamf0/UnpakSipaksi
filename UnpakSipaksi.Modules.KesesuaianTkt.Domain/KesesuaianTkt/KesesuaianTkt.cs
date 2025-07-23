using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt
{
    public sealed partial class KesesuaianTkt : Entity
    {
        private KesesuaianTkt()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Skor { get; private set; }

        public static KesesuaianTktBuilder Update(KesesuaianTkt prev) => new KesesuaianTktBuilder(prev);

        public static Result<KesesuaianTkt> Create(
        string Nama,
        int Skor
        )
        {
            if (Skor < 0)
            {
                return Result.Failure<KesesuaianTkt>(KesesuaianTktErrors.InvalidValueSkor());
            }
            var asset = new KesesuaianTkt
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor
            };

            asset.Raise(new KesesuaianTktCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
