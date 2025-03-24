namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys
{
    public interface IModelFeasibilityStudysRepository
    {
        void Insert(ModelFeasibilityStudys ModelFeasibilityStudys);
        Task<ModelFeasibilityStudys> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(ModelFeasibilityStudys ModelFeasibilityStudys);
    }
}
