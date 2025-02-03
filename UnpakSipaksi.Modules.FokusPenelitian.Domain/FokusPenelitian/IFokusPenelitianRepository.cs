using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian
{
    public interface IFokusPenelitianRepository
    {
        void Insert(FokusPenelitian FokusPenelitian);
        Task<FokusPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(FokusPenelitian FokusPenelitian);
    }
}
