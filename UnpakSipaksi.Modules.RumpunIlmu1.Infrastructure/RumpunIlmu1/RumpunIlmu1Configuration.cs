using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.RumpunIlmu1
{
    internal sealed class RumpunIlmu1Configuration : IEntityTypeConfiguration<Domain.RumpunIlmu1.RumpunIlmu1>
    {
        public void Configure(EntityTypeBuilder<Domain.RumpunIlmu1.RumpunIlmu1> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
