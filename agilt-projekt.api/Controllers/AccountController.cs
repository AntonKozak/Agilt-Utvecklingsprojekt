using EventApi.Auth;
using EventApi.Services;
using EventApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EventApi.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{

    private readonly UserManager<UserModel> _userManager;
    private readonly TokenService _tokenService;
    public AccountController(UserManager<UserModel> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if(user is null || !await _userManager.CheckPasswordAsync(user,model.Password)){
            return Unauthorized();
        }


        return Ok(new UserViewModel

        {
            Email = user.Email, Token = await _tokenService.CreateToken(user)
        });

    }

    [HttpPost("register")]

    public async Task<IActionResult> Register(RegisterViewModel model){


        var user = new UserModel{
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };


        var result = await _userManager.CreateAsync(user, model.Password);

        if(!result.Succeeded){
            foreach(var error in result.Errors){
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }
        var roles = new List<string>
        {
            "User"
        };
        await _userManager.AddToRolesAsync(user,roles);

        return StatusCode(201);
    }
}