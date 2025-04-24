using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.PenelitianHibah
{
    internal sealed class PenelitianHibahConfiguration : IEntityTypeConfiguration<Domain.PenelitianHibah.PenelitianHibah>
    {
        public void Configure(EntityTypeBuilder<Domain.PenelitianHibah.PenelitianHibah> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
