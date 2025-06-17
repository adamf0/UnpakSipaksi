using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public static class InsentifCalculator
    {
        public static Result<InsentifResult> Hitung(InsentifInput input)
        {
            if (input.PeranPenulis.Equals(Peran.CoAuthor) && input.JumlahCoAuthor<=1) {
                return Result.Failure<InsentifResult>(InsentifErrors.InvalidDataVerifikasi());
            }

            InsentifResult result = new InsentifResult();

            // Aturan porsi SBU
            (decimal ifaRatio, decimal icaRatio, int porsi) = GetRatio(input.JenisJurnal, input.Mahasiswa, input.PeranPenulis);

            result.IFA = input.SBU * ifaRatio;
            result.ICA = input.SBU * icaRatio;
            result.PorsiSBU = porsi;

            if (input.PeranPenulis == Peran.FirstAuthor || input.JumlahCoAuthor <= 1)
            {
                result.TotalInsentif = result.IFA + result.ICA;
            }
            else
            {
                result.TotalInsentif = (result.IFA + result.ICA) / (input.JumlahCoAuthor - 1);
            }

            return result;
        }

        private static (decimal ifaRatio, decimal icaRatio, int porsiSBU) GetRatio(JenisJurnal jenis, bool mahasiswa, Peran peran)
        {
            return (jenis, mahasiswa, peran) switch
            {
                (JenisJurnal.External, false, Peran.FirstAuthor) => (0.6m, 0m, 60),
                (JenisJurnal.External, false, Peran.CoAuthor) => (0m, 0.4m, 40),
                (JenisJurnal.External, true, Peran.FirstAuthor) => (0.3m, 0m, 30),
                (JenisJurnal.External, true, Peran.CoAuthor) => (0m, 0.2m, 20),

                (JenisJurnal.Internal, false, Peran.FirstAuthor) => (0.3m, 0m, 30),
                (JenisJurnal.Internal, false, Peran.CoAuthor) => (0m, 0.2m, 20),
                (JenisJurnal.Internal, true, Peran.FirstAuthor) => (0.15m, 0m, 15),
                (JenisJurnal.Internal, true, Peran.CoAuthor) => (0m, 0.1m, 10),

                _ => throw new ArgumentException("Kombinasi input tidak valid")
            };
        }
    }

}
