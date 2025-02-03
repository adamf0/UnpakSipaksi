using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.AkurasiPenelitian
{
    internal sealed class AkurasiPenelitianConfiguration : IEntityTypeConfiguration<Domain.AkurasiPenelitian.AkurasiPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.AkurasiPenelitian.AkurasiPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
