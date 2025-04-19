using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApi;
using UnpakSipaksi.Modules.KesesuaianJadwal.PublicApi;
using UnpakSipaksi.Modules.KesesuaianPenugasan.PublicApi;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.PublicApi;
using UnpakSipaksi.Modules.KetajamanAnalisis.PublicApi;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.PublicApi;
using UnpakSipaksi.Modules.KualitasIpteks.PublicApi;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.PubliApi;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.PubliApi;
using UnpakSipaksi.Modules.KuantitasStatusKi.PublicApi;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.PublicApi;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.PublicApi;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.PublicApi;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.PublicApi;
using UnpakSipaksi.Modules.VideoKegiatan.PublicApi;

namespace UnpakSipaksi.Modules.SubstansiBobot.Application.GetSubstansiBobot
{
    internal sealed class GetSubstansiBobotPkmQueryHandler(
            //IDbConnectionFactory _dbConnectionFactory
            IKualitasKuantitasPublikasiJurnalIlmiahApi KualitasKuantitasPublikasiJurnalIlmiahApi,
            IKualitasKuantitasPublikasiProsidingApi KualitasKuantitasPublikasiProsidingApi,
            IKuantitasStatusKiApi KuantitasStatusKIApi,
            IKetajamanAnalisisApi KetajamanAnalisisApi,
            IRumusanPrioritasMitraApi RumusanPrioritasMitraApi,
            IKesesuaianSolusiMasalahMitraApi KesesuaianSolusiMasalahMitraApi,
            IMetodeRencanaKegiatanApi MetodeRencanaKegiatanApi,
            IKesesuaianPenugasanApi KesesuaianPenugasanApi,
            IKualitasIpteksApi KualitasIpteksApi,
            ILuaranArtikelApi LuaranArtikelApi,
            IArtikelMediaMassaApi ArtikelMediaMassaApi,
            IVideoKegiatanApi VideoKegiatanApi,
            IPeningkatanKeberdayaanMitraApi PeningkatanKeberdayaanMitraApi,
            IKewajaranTahapanTargetApi KewajaranTahapanTargetApi,
            IKesesuaianJadwalApi KesesuaianJadwalApi
        ) : IQueryHandler<GetSubstansiBobotPKMQuery, SubstansiBobotPKMResponse>
    {
        public async Task<Result<SubstansiBobotPKMResponse>> Handle(GetSubstansiBobotPKMQuery request, CancellationToken cancellationToken)
        {
            var jurnalIlmiahTask            = KualitasKuantitasPublikasiJurnalIlmiahApi.GetBobotWithoutTargetAsync();
            var prosidingTask               = KualitasKuantitasPublikasiProsidingApi.GetBobotWithoutTargetAsync();
            var statusKiTask                = KuantitasStatusKIApi.GetBobotWithoutTargetAsync();
            var ketajamanAnalisisTask       = KetajamanAnalisisApi.GetBobotWithoutTargetAsync();
            var rumusanPrioritasMitraTask   = RumusanPrioritasMitraApi.GetBobotWithoutTargetAsync();
            var solusiMasalahMitraTask      = KesesuaianSolusiMasalahMitraApi.GetBobotWithoutTargetAsync();
            var metodeRencanaKegiatanTask   = MetodeRencanaKegiatanApi.GetBobotWithoutTargetAsync();
            var penugasanTask               = KesesuaianPenugasanApi.GetBobotWithoutTargetAsync();
            var kualitasIpteksTask          = KualitasIpteksApi.GetBobotWithoutTargetAsync();
            var luaranArtikelTask           = LuaranArtikelApi.GetBobotWithoutTargetAsync();
            var artikelMediaMassaTask       = ArtikelMediaMassaApi.GetBobotWithoutTargetAsync();
            var videoKegiatanTask           = VideoKegiatanApi.GetBobotWithoutTargetAsync();
            var keberdayaanMitraTask        = PeningkatanKeberdayaanMitraApi.GetBobotWithoutTargetAsync();
            var kewajaranTargetTask         = KewajaranTahapanTargetApi.GetBobotWithoutTargetAsync();
            var kesesuaianJadwalTask        = KesesuaianJadwalApi.GetBobotWithoutTargetAsync();

            await Task.WhenAll(
                jurnalIlmiahTask,
                prosidingTask,
                statusKiTask,
                ketajamanAnalisisTask,
                rumusanPrioritasMitraTask,
                solusiMasalahMitraTask,
                metodeRencanaKegiatanTask,
                penugasanTask,
                kualitasIpteksTask,
                luaranArtikelTask,
                artikelMediaMassaTask,
                videoKegiatanTask,
                keberdayaanMitraTask,
                kewajaranTargetTask,
                kesesuaianJadwalTask
            );

            int KualitasKuantitasPublikasiJurnalIlmiah  = jurnalIlmiahTask.Result ?? 0;
            int KualitasKuantitasPublikasiProsiding     = prosidingTask.Result ?? 0;
            int KuantitasStatusKI                       = statusKiTask.Result ?? 0;
            int KetajamanAnalisis                       = ketajamanAnalisisTask.Result ?? 0;
            int RumusanPrioritasMitra                   = rumusanPrioritasMitraTask.Result ?? 0;
            int KesesuaianSolusiMasalahMitra            = solusiMasalahMitraTask.Result ?? 0;
            int MetodeRencanaKegiatan                   = metodeRencanaKegiatanTask.Result ?? 0;
            int KesesuaianPenugasan                     = penugasanTask.Result ?? 0;
            int KualitasIpteks                          = kualitasIpteksTask.Result ?? 0;
            int LuaranArtikel                           = luaranArtikelTask.Result ?? 0;
            int ArtikelMediaMassa                       = artikelMediaMassaTask.Result ?? 0;
            int VideoKegiatan                           = videoKegiatanTask.Result ?? 0;
            int PeningkatanKeberdayaanMitra             = keberdayaanMitraTask.Result ?? 0;
            int KewajaranTahapanTarget                  = kewajaranTargetTask.Result ?? 0;
            int KesesuaianJadwal                        = kesesuaianJadwalTask.Result ?? 0;



            return Result.Success(new SubstansiBobotPKMResponse
            {
                KualitasKuantitasPublikasiJurnalIlmiah  = KualitasKuantitasPublikasiJurnalIlmiah,
                KualitasKuantitasPublikasiProsiding     = KualitasKuantitasPublikasiProsiding,
                KuantitasStatusKI                       = KuantitasStatusKI,
                KetajamanAnalisis                       = KetajamanAnalisis,
                RumusanPrioritasMitra                   = RumusanPrioritasMitra,
                KesesuaianSolusiMasalahMitra            = KesesuaianSolusiMasalahMitra,
                MetodeRencanaKegiatan                   = MetodeRencanaKegiatan,
                KesesuaianPenugasan                     = KesesuaianPenugasan,
                KualitasIpteks                          = KualitasIpteks,
                LuaranArtikel                           = LuaranArtikel,
                ArtikelMediaMassa                       = ArtikelMediaMassa,
                VideoKegiatan                           = VideoKegiatan,
                PeningkatanKeberdayaanMitra             = PeningkatanKeberdayaanMitra,
                KewajaranTahapanTarget                  = KewajaranTahapanTarget,
                KesesuaianJadwal                        = KesesuaianJadwal
            });

        }
    }
}
