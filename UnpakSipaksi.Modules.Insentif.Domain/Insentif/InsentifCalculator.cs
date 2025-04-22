using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public class InsentifCalculator
    {
        private decimal _sbu;
        private int _jumlahCoAuthor;

        public decimal IFA { get; private set; }
        public decimal ICA { get; private set; }
        public decimal TotalInsentif { get; private set; }
        public int PorsiSBU { get; private set; }

        public InsentifCalculator(decimal sbu, int jumlahCoAuthor)
        {
            _sbu = sbu;
            _jumlahCoAuthor = jumlahCoAuthor;
        }

        public void HitungInsentif(
            JenisJurnal jenisJurnal,
            LibatkanMahasiswa mahasiswa,
            Peran peran)
        {
            SetPorsi(jenisJurnal, mahasiswa, peran);

            if (peran == Peran.FirstAuthor)
            {
                TotalInsentif = IFA + ICA;
            }
            else if (peran == Peran.CoAuthor)
            {
                if (_jumlahCoAuthor == 1)
                {
                    // Auto switch to First Author logic
                    SetPorsi(jenisJurnal, mahasiswa, Peran.FirstAuthor);
                    TotalInsentif = IFA + ICA;
                }
                else
                {
                    TotalInsentif = (IFA + ICA) / (_jumlahCoAuthor - 1);
                }
            }
        }

        private void SetPorsi(
            JenisJurnal jenisJurnal,
            LibatkanMahasiswa mahasiswa,
            Peran peran)
        {
            if (jenisJurnal == JenisJurnal.External)
            {
                if (mahasiswa == LibatkanMahasiswa.Tidak && peran == Peran.FirstAuthor)
                {
                    IFA = _sbu * 0.6m;
                    ICA = 0;
                    PorsiSBU = 60;
                }
                else if (mahasiswa == LibatkanMahasiswa.Tidak && peran == Peran.CoAuthor)
                {
                    IFA = 0;
                    ICA = _sbu * 0.4m;
                    PorsiSBU = 40;
                }
                else if (mahasiswa == LibatkanMahasiswa.Ya && peran == Peran.FirstAuthor)
                {
                    IFA = _sbu * 0.3m;
                    ICA = 0;
                    PorsiSBU = 30;
                }
                else if (mahasiswa == LibatkanMahasiswa.Ya && peran == Peran.CoAuthor)
                {
                    IFA = 0;
                    ICA = _sbu * 0.2m;
                    PorsiSBU = 20;
                }
            }
            else if (jenisJurnal == JenisJurnal.Internal)
            {
                if (mahasiswa == LibatkanMahasiswa.Tidak && peran == Peran.FirstAuthor)
                {
                    IFA = _sbu * 0.3m;
                    ICA = 0;
                    PorsiSBU = 30;
                }
                else if (mahasiswa == LibatkanMahasiswa.Tidak && peran == Peran.CoAuthor)
                {
                    IFA = 0;
                    ICA = _sbu * 0.2m;
                    PorsiSBU = 20;
                }
                else if (mahasiswa == LibatkanMahasiswa.Ya && peran == Peran.FirstAuthor)
                {
                    IFA = _sbu * 0.15m;
                    ICA = 0;
                    PorsiSBU = 15;
                }
                else if (mahasiswa == LibatkanMahasiswa.Ya && peran == Peran.CoAuthor)
                {
                    IFA = 0;
                    ICA = _sbu * 0.1m;
                    PorsiSBU = 10;
                }
            }
        }
    }
}
