namespace UnpakSipaksi.Modules.PenelitianPkm.PublicApi
{
    public sealed record SkemaDefaultResponse(
        string IdSkema,
        string UuidSkema,
        string NamaSkema,
        string TKT,
        string IdKategoriTkt,
        string UuidKategoriTkt,
        string NamaKategoriTkt
    );

}
