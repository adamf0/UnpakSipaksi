using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim
{
    public sealed partial class KejelasanPembagianTugasTim : Entity
    {
        private KejelasanPembagianTugasTim()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }
        [Column("name")]
        public string Nama { get; private set; } = null!;
        public int Skor { get; private set; }

        public static KejelasanPembagianTugasTimBuilder Update(KejelasanPembagianTugasTim prev) => new KejelasanPembagianTugasTimBuilder(prev);

        public static Result<KejelasanPembagianTugasTim> Create(
        string Nama,
        int Skor
        )
        {
            var asset = new KejelasanPembagianTugasTim
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Skor = Skor,
            };

            asset.Raise(new KejelasanPembagianTugasTimCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
