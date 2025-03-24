using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.RumpunIlmu2
{
    internal sealed class RumpunIlmu2Configuration : IEntityTypeConfiguration<Domain.RumpunIlmu2.RumpunIlmu2>
    {
        public void Configure(EntityTypeBuilder<Domain.RumpunIlmu2.RumpunIlmu2> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
