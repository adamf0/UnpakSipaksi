namespace UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi
{
    public interface IKuantitasStatusKiRepository
    {
        void Insert(KuantitasStatusKi KuantitasStatusKi);
        Task<KuantitasStatusKi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KuantitasStatusKi KuantitasStatusKi);
    }
}
