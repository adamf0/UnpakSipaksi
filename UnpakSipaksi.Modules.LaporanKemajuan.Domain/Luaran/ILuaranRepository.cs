namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran
{
    public interface ILuaranRepository
    {
        void Insert(Luaran Luaran);
        Task<Luaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Luaran Luaran);
    }
}
