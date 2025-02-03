using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian
{
    public sealed partial class TopikPenelitian : Entity
    {
        private TopikPenelitian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;

        public int TemaPenelitianId { get; private set; }

        public static TopikPenelitianBuilder Update(TopikPenelitian prev) => new TopikPenelitianBuilder(prev);

        public static Result<TopikPenelitian> Create(
        string Nama,
        int TemaPenelitianId
        )
        {
            var asset = new TopikPenelitian
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                TemaPenelitianId = TemaPenelitianId
            };

            asset.Raise(new TopikPenelitianCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
