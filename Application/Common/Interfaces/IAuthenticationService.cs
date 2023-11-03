namespace Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<bool> IsAuthenticated();
    
    Task<string> ValidateJwtToken(string jwtToken);
    
    Task<string> GetCurrentUser();
    
    Task<string> GetCurrentUserId();
}   