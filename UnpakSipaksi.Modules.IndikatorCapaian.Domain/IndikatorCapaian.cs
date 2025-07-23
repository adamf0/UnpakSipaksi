using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Domain
{
    public sealed partial class IndikatorCapaian : Entity
    {
        private IndikatorCapaian()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }

        [Column("id_jenis_luaran")]
        public int JenisLuaranId { get; private set; }
        [Column("nama")]
        public string Nama { get; private set; } = null!;
        [Column("status")]
        public string? Status { get; private set; }

        public static Result<IndikatorCapaian> Create(
        int JenisLuaranId,
        string Nama,
        string? Status
        )
        {
            if (JenisLuaranId <= 0)
            {
                return Result.Failure<IndikatorCapaian>(IndikatorCapaianErrors.UnknownJenisLuaran());
            }

            var asset = new IndikatorCapaian
            {
                Uuid = Guid.NewGuid(),
                JenisLuaranId = JenisLuaranId,
                Nama = Nama,
                Status = Status
            };

            asset.Raise(new IndikatorCapaianCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<IndikatorCapaian> Update(
        Guid id,
        int JenisLuaranId,
        string Nama,
        string? Status,
        IndikatorCapaian? prev
        )
        {
            if (prev == null)
            {
                return Result.Failure<IndikatorCapaian>(IndikatorCapaianErrors.NotFound(id));
            }
            if (JenisLuaranId <= 0)
            {
                return Result.Failure<IndikatorCapaian>(IndikatorCapaianErrors.UnknownJenisLuaran());
            }

            prev.JenisLuaranId = JenisLuaranId;
            prev.Nama = Nama;
            prev.Status = Status;

            return prev;
        }
    }
}
