using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian
{
    public sealed partial class FokusPengabdian : Entity
    {
        private FokusPengabdian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public static FokusPengabdianBuilder Update(FokusPengabdian prev) => new FokusPengabdianBuilder(prev);

        public static Result<FokusPengabdian> Create(
        string Nama
        )
        {
            var asset = new FokusPengabdian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama
            };

            asset.Raise(new FokusPengabdianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
