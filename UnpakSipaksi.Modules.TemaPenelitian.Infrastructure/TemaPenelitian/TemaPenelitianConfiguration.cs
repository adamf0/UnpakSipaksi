using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.TemaPenelitian
{
    internal sealed class TemaPenelitianConfiguration : IEntityTypeConfiguration<Domain.TemaPenelitian.TemaPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.TemaPenelitian.TemaPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
