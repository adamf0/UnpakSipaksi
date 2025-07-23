using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi
{
    public sealed partial class JenisPublikasi : Entity
    {
        private JenisPublikasi()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid{ get; private set; }

        public string Nama { get; private set; } = null!;
        public int Sbu { get; private set; } = 0;
        
        public static JenisPublikasiBuilder Update(JenisPublikasi prev) => new JenisPublikasiBuilder(prev);

        public static Result<JenisPublikasi> Create(
        string Nama,
        int Sbu
        )
        {
            if (Sbu < 0) {
                return Result.Failure<JenisPublikasi>(JenisPublikasiErrors.InvalidSbu());
            }
            var asset = new JenisPublikasi
            {
                Uuid = Guid.NewGuid(),
                Nama = Nama,
                Sbu = Sbu
            };

            asset.Raise(new JenisPublikasiCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
