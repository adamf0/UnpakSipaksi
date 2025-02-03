using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriTkt.Infrastructure.KategoriTkt
{
    internal sealed class KategoriTktConfiguration : IEntityTypeConfiguration<Domain.KategoriTkt.KategoriTkt>
    {
        public void Configure(EntityTypeBuilder<Domain.KategoriTkt.KategoriTkt> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
