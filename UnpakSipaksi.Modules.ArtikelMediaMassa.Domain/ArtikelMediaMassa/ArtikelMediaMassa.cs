using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa
{
    public sealed partial class ArtikelMediaMassa : Entity
    {
        private ArtikelMediaMassa()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        [Column("nama")]
        public string Nama { get; private set; } = null!;
        public int Nilai { get; private set; } = 0;
        
        public static ArtikelMediaMassaBuilder Update(ArtikelMediaMassa prev) => new ArtikelMediaMassaBuilder(prev);

        public static Result<ArtikelMediaMassa> Create(
        string Nama,
        int Nilai
        )
        {
            if (Nilai < 0 || Nilai > int.MaxValue)
            {
                return Result.Failure<ArtikelMediaMassa>(ArtikelMediaMassaErrors.InvalidNilai());
            }

            var asset = new ArtikelMediaMassa
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Nilai = Nilai,
            };

            asset.Raise(new ArtikelMediaMassaCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
