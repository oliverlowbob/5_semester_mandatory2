namespace Skoleprotokol.Services
{
    public interface IJwtService<TUser>
    {
        abstract string GenerateAccessToken(TUser user);
    }
}