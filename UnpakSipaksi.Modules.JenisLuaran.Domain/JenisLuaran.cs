using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Domain
{
    public sealed partial class JenisLuaran : Entity
    {
        private JenisLuaran()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("nama")]
        public string Nama { get; private set; } = null!;

        public static Result<JenisLuaran> Create(
        string Nama
        )
        {
            var asset = new JenisLuaran
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama
            };

            asset.Raise(new JenisLuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<JenisLuaran> Update(
        Guid id,
        string Nama,
        JenisLuaran prev
        )
        {
            if (prev == null) {
                return Result.Failure<JenisLuaran>(JenisLuaranErrors.NotFound(id));
            }

            prev.Nama = Nama;

            return prev;
        }
    }
}
