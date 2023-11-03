namespace Application.Common.Interfaces;

public interface IAuthorizationService
{
    Task<bool> IsInRoleAsync(string user, string role);
    
    Task<bool> AuthorizeAsync(string user, string policyName);
}