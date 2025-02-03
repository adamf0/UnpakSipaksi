using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.FokusPenelitian
{
    internal sealed class FokusPenelitianConfiguration : IEntityTypeConfiguration<Domain.FokusPenelitian.FokusPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.FokusPenelitian.FokusPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
