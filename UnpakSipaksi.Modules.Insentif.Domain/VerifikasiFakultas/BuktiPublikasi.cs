using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas
{
    public enum JenisJurnal
    {
        [EnumMember(Value = "internal")]
        Internal = 1,

        [EnumMember(Value = "external")]
        External = 0
    }
}
