using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1
{
    public sealed partial class RumpunIlmu1 : Entity
    {
        private RumpunIlmu1()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public static RumpunIlmu1Builder Update(RumpunIlmu1 prev) => new RumpunIlmu1Builder(prev);

        public static Result<RumpunIlmu1> Create(
        string Nama
        )
        {
            var asset = new RumpunIlmu1
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
            };

            asset.Raise(new RumpunIlmu1CreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
