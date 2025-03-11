namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan
{
    public interface IKredibilitasMitraDukunganRepository
    {
        void Insert(KredibilitasMitraDukungan KredibilitasMitraDukungan);
        Task<KredibilitasMitraDukungan> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KredibilitasMitraDukungan KredibilitasMitraDukungan);
    }
}
