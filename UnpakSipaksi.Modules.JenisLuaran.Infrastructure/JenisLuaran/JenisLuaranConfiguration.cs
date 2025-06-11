using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisLuaran.Infrastructure.JenisLuaran
{
    internal sealed class JenisLuaranConfiguration : IEntityTypeConfiguration<Domain.JenisLuaran>
    {
        public void Configure(EntityTypeBuilder<Domain.JenisLuaran> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
