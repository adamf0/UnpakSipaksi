using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer
{
    public sealed class PenugasanReviewerCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
