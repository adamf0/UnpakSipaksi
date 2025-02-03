using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian
{
    public sealed partial class TemaPenelitian : Entity
    {
        private TemaPenelitian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public int FokusPenelitianId { get; private set; }

        public static TemaPenelitianBuilder Update(TemaPenelitian prev) => new TemaPenelitianBuilder(prev);

        public static Result<TemaPenelitian> Create(
        string Nama,
        int FokusPenelitianId
        )
        {
            var asset = new TemaPenelitian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                FokusPenelitianId = FokusPenelitianId
            };

            asset.Raise(new TemaPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
