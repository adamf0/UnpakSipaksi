using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas
{
    public enum BuktiPublikasi
    {
        [EnumMember(Value = "ya")]
        Ya = 1,

        [EnumMember(Value = "tidak")]
        Tidak = 0
    }
}
