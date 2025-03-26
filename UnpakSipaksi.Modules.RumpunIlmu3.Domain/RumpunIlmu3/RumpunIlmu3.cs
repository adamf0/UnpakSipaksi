using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3
{
    public sealed partial class RumpunIlmu3 : Entity
    {
        private RumpunIlmu3()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int IdRumpunIlmu2 { get; private set; }

        public static RumpunIlmu3Builder Update(RumpunIlmu3 prev) => new RumpunIlmu3Builder(prev);

        public static Result<RumpunIlmu3> Create(
        string Nama,
        int IdRumpunIlmu2
        )
        {
            var asset = new RumpunIlmu3
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                IdRumpunIlmu2 = IdRumpunIlmu2
            };

            asset.Raise(new RumpunIlmu3CreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
