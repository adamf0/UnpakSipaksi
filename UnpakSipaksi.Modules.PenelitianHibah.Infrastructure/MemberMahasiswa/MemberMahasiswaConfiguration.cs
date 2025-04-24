using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberMahasiswa
{
    internal sealed class MemberMahasiswaConfiguration : IEntityTypeConfiguration<Domain.MemberMahasiswa.MemberMahasiswa>
    {
        public void Configure(EntityTypeBuilder<Domain.MemberMahasiswa.MemberMahasiswa> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}
