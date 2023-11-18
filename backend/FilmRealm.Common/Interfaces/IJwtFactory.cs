using FilmRealm.Common.JWT;

namespace FilmRealm.Common.Interfaces;

public interface IJwtFactory
{
    Task<AccessToken> GenerateAccessToken(int id, string userName, string email);
    string GenerateRefreshToken();
    int GetUserIdFromToken(string accessToken, string signingKey);
}