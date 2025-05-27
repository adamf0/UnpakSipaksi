using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Kategori.Infrastructure.Kategori
{
    internal sealed class KategoriConfiguration : IEntityTypeConfiguration<Domain.Kategori.Kategori>
    {
        public void Configure(EntityTypeBuilder<Domain.Kategori.Kategori> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
