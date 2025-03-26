namespace UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan
{
    public interface IVideoKegiatanRepository
    {
        void Insert(VideoKegiatan VideoKegiatan);
        Task<VideoKegiatan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(VideoKegiatan VideoKegiatan);
    }
}
