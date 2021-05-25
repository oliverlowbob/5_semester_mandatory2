namespace Skoleprotokol.Utils
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int ExpiresInMilliseconds { get; set; }
    }
}
