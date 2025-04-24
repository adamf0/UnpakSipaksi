using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberDosen
{
    internal sealed class MemberDosenConfiguration : IEntityTypeConfiguration<Domain.MemberDosen.MemberDosen>
    {
        public void Configure(EntityTypeBuilder<Domain.MemberDosen.MemberDosen> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
