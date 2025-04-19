using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Roadmap.Application.DeleteRoadmap
{
    public sealed record DeleteRoadmapCommand(
        Guid uuid
    ) : ICommand;
}
