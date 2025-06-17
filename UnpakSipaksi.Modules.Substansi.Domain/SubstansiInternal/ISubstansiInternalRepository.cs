namespace UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal
{
    public interface ISubstansiInternalRepository
    {
        void Insert(SubstansiInternal Substansi);
        Task<SubstansiInternal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(SubstansiInternal Substansi);
    }
}
