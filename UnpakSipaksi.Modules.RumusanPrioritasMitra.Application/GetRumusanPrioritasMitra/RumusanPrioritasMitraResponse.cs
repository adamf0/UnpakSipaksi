namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra
{
    public sealed record RumusanPrioritasMitraResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
