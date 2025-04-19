using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public enum AnnouncementType
    {
        [EnumMember(Value = "pengumuman")]
        Pengumuman,

        [EnumMember(Value = "notifikasi")]
        Notifikasi
    }
}
