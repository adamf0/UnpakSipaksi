using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Roadmap.Application.UpdateRoadmap
{
    public sealed record UpdateRoadmapCommand(
        Guid Uuid,
        string Nidn,
        string Link
    ) : ICommand;
}
