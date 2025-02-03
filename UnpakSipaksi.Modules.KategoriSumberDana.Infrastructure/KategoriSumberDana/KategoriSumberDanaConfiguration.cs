using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.KategoriSumberDana
{
    internal sealed class KategoriSumberDanaConfiguration : IEntityTypeConfiguration<Domain.KategoriSumberDana.KategoriSumberDana>
    {
        public void Configure(EntityTypeBuilder<Domain.KategoriSumberDana.KategoriSumberDana> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
