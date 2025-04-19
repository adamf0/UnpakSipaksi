using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public class Attachment
    {
        public string? Path { get; }
        public string? Link { get; }

        private Attachment(string? path, string? link)
        {
            Path = path;
            Link = link;
        }

        public static Attachment FromPath(string path)
        {
            return new Attachment(path, null);
        }

        public static Attachment FromUrl(string url)
        {
            return new Attachment(null, url);
        }

        public bool IsEmpty() => Path == string.Empty && Link == string.Empty;
    }
}
