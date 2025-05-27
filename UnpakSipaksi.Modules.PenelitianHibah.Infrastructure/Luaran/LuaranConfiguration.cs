using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Luaran
{
    internal sealed class LuaranConfiguration : IEntityTypeConfiguration<Domain.Luaran.Luaran>
    {
        public void Configure(EntityTypeBuilder<Domain.Luaran.Luaran> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
