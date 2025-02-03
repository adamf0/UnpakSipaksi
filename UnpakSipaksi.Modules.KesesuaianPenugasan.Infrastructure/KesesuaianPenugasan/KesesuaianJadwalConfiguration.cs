using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.KesesuaianPenugasan
{
    internal sealed class KesesuaianPenugasanConfiguration : IEntityTypeConfiguration<Domain.KesesuaianPenugasan.KesesuaianPenugasan>
    {
        public void Configure(EntityTypeBuilder<Domain.KesesuaianPenugasan.KesesuaianPenugasan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
