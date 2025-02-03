using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.AuthorSinta.Infrastructure.AuthorSinta
{
    internal sealed class AuthorSintaConfiguration : IEntityTypeConfiguration<Domain.AuthorSinta.AuthorSinta>
    {
        public void Configure(EntityTypeBuilder<Domain.AuthorSinta.AuthorSinta> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
