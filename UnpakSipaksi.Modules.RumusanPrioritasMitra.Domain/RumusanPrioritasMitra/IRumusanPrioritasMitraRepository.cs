namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra
{
    public interface IRumusanPrioritasMitraRepository
    {
        void Insert(RumusanPrioritasMitra RumusanPrioritasMitra);
        Task<RumusanPrioritasMitra> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RumusanPrioritasMitra RumusanPrioritasMitra);
    }
}
