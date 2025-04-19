using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Metode.Domain.Metode
{
    public static class MetodeErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Metode.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Metode.NotFound", $"Metode with the identifier {Id} was not found");

        public static Error AkurasiPenelitianNotFound() =>
            Error.NotFound("Metode.AkurasiPenelitianNotFound", "data AkurasiPenelitian is not found");

        public static Error KejelasanPembagianTugasTimTaskNotFound() =>
            Error.NotFound("Metode.KejelasanPembagianTugasTimTaskNotFound", "data KejelasanPembagianTugasTimTask is not found");

        public static Error KesesuaianWaktuRabLuaranFasilitasNotFound() =>
            Error.NotFound("Metode.KesesuaianWaktuRabLuaranFasilitasNotFound", "data KesesuaianWaktuRabLuaranFasilitas is not found");

        public static Error PotensiKetercapaianLuaranDijanjikanNotFound() =>
            Error.NotFound("Metode.PotensiKetercapaianLuaranDijanjikanNotFound", "data PotensiKetercapaianLuaranDijanjikan is not found");

        public static Error ModelFeasibilityStudyNotFound() =>
            Error.NotFound("Metode.ModelFeasibilityStudyNotFound", "data ModelFeasibilityStudy is not found");
        public static Error KesesuaianTktNotFound() =>
            Error.NotFound("Metode.KesesuaianTktNotFound", "data KesesuaianTkt is not found");
        public static Error KredibilitasMitraDukunganNotFound() =>
            Error.NotFound("Metode.KredibilitasMitraDukunganNotFound", "data ModelFeasibilityStudy is not found");

    }
}
