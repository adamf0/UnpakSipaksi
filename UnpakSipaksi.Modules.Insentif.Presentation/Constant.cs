using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Presentation
{
    public static class Constant
    {
        public const string jurnalKey = "jurnal";
        public const string prosidingKey = "prosiding";
        
        public const string externalKey = "external";
        private const int externalVal = 0;
        public const string internalKey = "internal";
        private const int internalVal = 1;

        public const string yaKey = "ya";
        private const int yaVal = 1;
        public const string tidakKey = "tidak";
        private const int tidakVal = 0;

        public static List<string> listJenisJurnal = new List<string> { externalKey, internalKey };
        public static List<string> listLibatkanMahasiswa = new List<string> { yaKey, tidakKey };

        public static Dictionary<string, int> filterJenisJurnal = new(StringComparer.OrdinalIgnoreCase)
        {
            { externalKey, externalVal },
            { internalKey, internalVal }
        };
        public static Dictionary<string, int> filterLibatkanMahasiswa = new(StringComparer.OrdinalIgnoreCase)
        {
            { tidakKey, tidakVal },
            { yaKey, yaVal }
        };
    }
}
