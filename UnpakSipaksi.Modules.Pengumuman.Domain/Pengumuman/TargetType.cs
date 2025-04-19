using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public enum TargetType
    {
        [EnumMember(Value = "all")]
        All,

        [EnumMember(Value = "target")]
        Target
    }
}
