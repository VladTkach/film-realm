namespace FilmRealm.Common.DTOs.Auth;

public class RefreshedAccessTokenDto
{
    public AccessToken AccessToken { get; }
    public string RefreshToken { get; }

    public RefreshedAccessTokenDto(AccessToken accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}