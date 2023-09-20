using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace EventApi.Auth;



// Skapa användare
public class UserModel : IdentityUser
{
    public string FirstName {get;set;}
    public string LastName {get;set;}
}