using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta
{
    public interface IAuthorSintaRepository
    {
        void Insert(AuthorSinta authorSinta);
        Task<AuthorSinta> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(AuthorSinta authorSinta);
    }
}
