using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.JenisPublikasi
{
    internal sealed class JenisPublikasiConfiguration : IEntityTypeConfiguration<Domain.JenisPublikasi.JenisPublikasi>
    {
        public void Configure(EntityTypeBuilder<Domain.JenisPublikasi.JenisPublikasi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
