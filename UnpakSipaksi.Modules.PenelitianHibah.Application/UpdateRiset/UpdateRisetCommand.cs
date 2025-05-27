using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRiset
{
    public sealed record UpdateRisetCommand(
          string UuidPenelitianHibah,
          string? PrioritasRiset,
          string? BidangFokusPenelitianId,
          string? BidangFokusPenelitianTemaId,
          string? BidangFokusPenelitianTemaTopikId
    ) : ICommand;
}
