using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public class AnnouncementInfo
    {
        public AnnouncementType Type { get; }
        public TargetType Target { get; }
        public string? Nidn { get; }
        public string? KodeFakultas { get; }

        private AnnouncementInfo(AnnouncementType type, TargetType target, string? nidn, string? kodeFakultas)
        {
            Type = type;
            Target = target;
            Nidn = nidn;
            KodeFakultas = kodeFakultas;
        }

        public static AnnouncementInfo CreateForDosen(AnnouncementType type, TargetType target, string? nidn)
        {
            return new AnnouncementInfo(type, target, nidn, null);
        }

        public static AnnouncementInfo CreateForFakultas(AnnouncementType type, TargetType target, string? kodeFakultas)
        {
            return new AnnouncementInfo(type, target, null, kodeFakultas);
        }
    }
}
