using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Logbook.Infrastructure.Logbook
{
    internal sealed class LogbookConfiguration : IEntityTypeConfiguration<Domain.Logbook.Logbook>
    {
        public void Configure(EntityTypeBuilder<Domain.Logbook.Logbook> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
