using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteSubstansiUsulan
{
    public sealed record DeleteSubstansiUsulanCommand(
        string Uuid,
        string UuidPenelitianHibah
    ) : ICommand;
}
