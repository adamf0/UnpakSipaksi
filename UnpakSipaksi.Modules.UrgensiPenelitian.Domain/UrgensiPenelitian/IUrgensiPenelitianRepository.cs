namespace UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian
{
    public interface IUrgensiPenelitianRepository
    {
        void Insert(UrgensiPenelitian UrgensiPenelitian);
        Task<UrgensiPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(UrgensiPenelitian UrgensiPenelitian);
    }
}
