using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.RumpunIlmu3
{
    internal sealed class RumpunIlmu3Configuration : IEntityTypeConfiguration<Domain.RumpunIlmu3.RumpunIlmu3>
    {
        public void Configure(EntityTypeBuilder<Domain.RumpunIlmu3.RumpunIlmu3> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
