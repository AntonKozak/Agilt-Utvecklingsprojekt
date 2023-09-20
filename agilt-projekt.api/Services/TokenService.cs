using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventApi.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EventApi.Services;


public class TokenService{


    private readonly UserManager<UserModel> _userManager;

    private readonly IConfiguration _config;

    public TokenService(UserManager<UserModel> userManager, IConfiguration config){
        _config = config;

        _userManager = userManager;
    }


    public async Task<string> CreateToken(UserModel user){



         // JWT Payload -  Datan vi vill paketera.
         var claims = new List<Claim>{
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("FirstName",user.FirstName),
            new Claim("LastName", user.LastName)
         };



         // Titta vilka roller som en användare är behörig till

        var roles = await _userManager.GetRolesAsync(user);

        // Hittas roller gör vi en ny claim och paketerar det med oss.
        foreach(var role in roles){
            claims.Add(new Claim(ClaimTypes.Role,role));
        }
        // --------------------- PAYLOAD DONE --------------------

        // Kryptering nedan
        // Skapar en ny symmetricSecurityKey genom att helt enkelt använda mig av min hemliga tokenKey.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["tokenSettings:tokenKey"]));
        //Kryptera nyckeln enligt algoritmen nedan
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


        var options = new JwtSecurityToken(
            issuer: null,
            audience: null,
            //paketera claims
            claims: claims,
            // När ska token gå ut
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials

        );



        return new JwtSecurityTokenHandler().WriteToken(options);

    }
}