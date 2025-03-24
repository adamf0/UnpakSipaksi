using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2
{
    public sealed partial class RumpunIlmu2 : Entity
    {
        private RumpunIlmu2()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int IdRumpunIlmu1 { get; private set; }

        public static RumpunIlmu2Builder Update(RumpunIlmu2 prev) => new RumpunIlmu2Builder(prev);

        public static Result<RumpunIlmu2> Create(
        string Nama,
        int IdRumpunIlmu1
        )
        {
            var asset = new RumpunIlmu2
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                IdRumpunIlmu1 = IdRumpunIlmu1
            };

            asset.Raise(new RumpunIlmu2CreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
