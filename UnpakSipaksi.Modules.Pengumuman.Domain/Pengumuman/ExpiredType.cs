using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public enum ExpiredType
    {
        [EnumMember(Value = "no expire")]
        NoExpire,

        [EnumMember(Value = null)]
        Range
    }
}
