using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.FokusPengabdian
{
    internal sealed class FokusPengabdianConfiguration : IEntityTypeConfiguration<Domain.FokusPengabdian.FokusPengabdian>
    {
        public void Configure(EntityTypeBuilder<Domain.FokusPengabdian.FokusPengabdian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
