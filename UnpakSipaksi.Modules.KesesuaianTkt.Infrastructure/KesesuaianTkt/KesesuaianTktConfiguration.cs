using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.KesesuaianTkt
{
    internal sealed class KesesuaianTktConfiguration : IEntityTypeConfiguration<Domain.KesesuaianTkt.KesesuaianTkt>
    {
        public void Configure(EntityTypeBuilder<Domain.KesesuaianTkt.KesesuaianTkt> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
