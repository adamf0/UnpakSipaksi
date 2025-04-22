using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public enum JenisJurnal
    {
        [EnumMember(Value = "internal")]
        Internal = 1,

        [EnumMember(Value = "external")]
        External = 0
    }
}
