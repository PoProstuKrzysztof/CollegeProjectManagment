using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Identity;
using CollegeProjectManagment.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProjectManagment.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IJwtService _jwtService;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
    {
        if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
        {
            ApplicationRole role = new ApplicationRole()
            {
                Name = registerDTO.Role.ToLower(),
                NormalizedName = registerDTO.Role.ToUpper()
            };

            await _roleManager.CreateAsync(role);
        }

        //Validation
        if (ModelState.IsValid == false)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        //Create user
        ApplicationUser user = new ApplicationUser()
        {
            Email = registerDTO.Email,
            UserName = registerDTO.Email,
            PersonName = registerDTO.PersonName
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (registerDTO.Role.Equals("leader"))
        {
            await _userManager.AddToRoleAsync(user, registerDTO.Role);
        }

        if (result.Succeeded)
        {
            //sign-in
            await _signInManager.SignInAsync(user, isPersistent: false);

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            return Ok(authenticationResponse);
        }
        else
        {
            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description)); //error1 | error2
            return Problem(errorMessage);
        }
    }

    [HttpGet]
    public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Ok(true);
        }
        else
        {
            return Ok(false);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
    {
        //Validation
        if (ModelState.IsValid == false)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return NoContent();
            }

            //sign-in
            await _signInManager.SignInAsync(user, isPersistent: false);

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            return Ok(authenticationResponse);
        }
        else
        {
            return Problem("Invalid email or password");
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> GetLogout()
    {
        await _signInManager.SignOutAsync();

        return NoContent();
    }
}