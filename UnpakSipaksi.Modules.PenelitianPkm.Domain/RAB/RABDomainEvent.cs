using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.RAB
{
    public sealed class RABCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
