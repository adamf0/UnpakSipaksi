using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.ArtikelMediaMassa
{
    internal sealed class ArtikelMediaMassaConfiguration : IEntityTypeConfiguration<Domain.ArtikelMediaMassa.ArtikelMediaMassa>
    {
        public void Configure(EntityTypeBuilder<Domain.ArtikelMediaMassa.ArtikelMediaMassa> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
