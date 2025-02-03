using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.TopikPenelitian
{
    internal sealed class TopikPenelitianConfiguration : IEntityTypeConfiguration<Domain.TopikPenelitian.TopikPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.TopikPenelitian.TopikPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
