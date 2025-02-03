using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.KesesuaianSolusiMasalahMitra
{
    internal sealed class KesesuaianSolusiMasalahMitraConfiguration : IEntityTypeConfiguration<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra>
    {
        public void Configure(EntityTypeBuilder<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
