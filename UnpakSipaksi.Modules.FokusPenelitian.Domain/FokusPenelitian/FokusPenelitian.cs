using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian
{
    public sealed partial class FokusPenelitian : Entity
    {
        private FokusPenelitian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public static FokusPenelitianBuilder Update(FokusPenelitian prev) => new FokusPenelitianBuilder(prev);

        public static Result<FokusPenelitian> Create(
        string Nama
        )
        {
            var asset = new FokusPenelitian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama
            };

            asset.Raise(new FokusPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
