using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;

namespace UnpakSipaksi.Modules.KategoriLuaran.Domain.KategoriLuaran
{
    public sealed partial class KategoriLuaran : Entity
    {
        private KategoriLuaran()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("id_pdp_kategori")]
        public int KategoriId { get; private set; }
        [Column("nama")]
        public string Nama { get; private set; } = null!;
        [Column("status")]
        public string Status { get; private set; } = null!;

        public static KategoriLuaranBuilder Update(KategoriLuaran prev) => new KategoriLuaranBuilder(prev);

        public static Result<KategoriLuaran> Create(
        int KategoriId,
        string Nama,
        string Status
        )
        {
            if (KategoriId==0) { 
                return Result.Failure<KategoriLuaran>(KategoriLuaranErrors.KategoriNotFound());
            }

            var asset = new KategoriLuaran
            {
                Uuid = Guid.NewGuid(),
                KategoriId = KategoriId,
                Nama = Nama,
                Status = Status,
            };

            asset.Raise(new KategoriLuaranCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
