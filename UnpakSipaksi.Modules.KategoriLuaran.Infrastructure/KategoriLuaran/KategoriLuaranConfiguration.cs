using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.KategoriLuaran
{
    internal sealed class KategoriLuaranConfiguration : IEntityTypeConfiguration<Domain.KategoriLuaran.KategoriLuaran>
    {
        public void Configure(EntityTypeBuilder<Domain.KategoriLuaran.KategoriLuaran> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
