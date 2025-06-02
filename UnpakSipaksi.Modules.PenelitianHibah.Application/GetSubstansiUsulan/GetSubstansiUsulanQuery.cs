using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetSubstansiUsulan
{
    public sealed record GetSubstansiUsulanQuery(string UuidPenelitianHibah) : IQuery<SubstansiUsulanResponse>;
}
