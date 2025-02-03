using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta
{
    public sealed record AuthorSintaResponse
    {
        public string Uuid { get; set; }
        public string Nidn { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public int Score { get; set; }
    }
}
