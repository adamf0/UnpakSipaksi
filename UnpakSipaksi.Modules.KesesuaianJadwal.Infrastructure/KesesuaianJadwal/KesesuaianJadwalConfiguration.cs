using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.KesesuaianJadwal
{
    internal sealed class KesesuaianJadwalConfiguration : IEntityTypeConfiguration<Domain.KesesuaianJadwal.KesesuaianJadwal>
    {
        public void Configure(EntityTypeBuilder<Domain.KesesuaianJadwal.KesesuaianJadwal> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
