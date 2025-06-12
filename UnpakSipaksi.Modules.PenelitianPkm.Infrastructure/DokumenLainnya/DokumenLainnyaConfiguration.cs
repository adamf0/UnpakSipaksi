using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenLainnya
{
    internal sealed class DokumenLainnyaConfiguration : IEntityTypeConfiguration<Domain.DokumenLainnya.DokumenLainnya>
    {
        public void Configure(EntityTypeBuilder<Domain.DokumenLainnya.DokumenLainnya> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
