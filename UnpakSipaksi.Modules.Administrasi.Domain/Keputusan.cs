using System.Runtime.Serialization;

namespace UnpakSipaksi.Modules.Administrasi.Domain
{
    public enum Keputusan
    {
        [EnumMember(Value = "perlu perbaikan")]
        PerluPerbaikan,

        /*[EnumMember(Value = "diterima dengan perbaikan minor")]
        DiterimaPerbaikanMinor,

        [EnumMember(Value = "diterima dengan perbaikan major")]
        DiterimaPerbaikanMajor,*/

        [EnumMember(Value = "diterima")]
        Diterima
    }
}
