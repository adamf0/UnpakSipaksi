using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.JumlahKolaboratorPublikasBereputasi
{
    internal sealed class JumlahKolaboratorPublikasBereputasiConfiguration : IEntityTypeConfiguration<Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi>
    {
        public void Configure(EntityTypeBuilder<Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
