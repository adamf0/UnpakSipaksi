namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget
{
    public sealed record KewajaranTahapanTargetResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Nilai { get; set; }
    }
}
