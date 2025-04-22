using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public enum LibatkanMahasiswa
    {
        [EnumMember(Value = "ya")]
        Ya = 1,

        [EnumMember(Value = "tidak")]
        Tidak = 0
    }
}
