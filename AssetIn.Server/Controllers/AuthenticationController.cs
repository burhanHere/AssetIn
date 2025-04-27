using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using AssetIn.Server.Repositories;
using AssetIn.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("AssetIn.Server/[controller]")]
public class AuthenticationController(ApplicationDbContext applicationDbContext,UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, EmailService emailService)
 : ControllerBase
{
    private readonly AuthenticationRepository _authenticationRepository = new(applicationDbContext,userManager, roleManager, configuration, emailService);

    [HttpPost(template: "UserSignUp")]
    public async Task<IActionResult> UserSignUp([FromBody] UserSignUpDTO userSignUpDTO)
    {
        ApiResponse result = await _authenticationRepository.UserSignUp(userSignUpDTO);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status409Conflict => Conflict(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpGet(template: "ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        ApiResponse result = await _authenticationRepository.ConfirmEmail(token, email);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status409Conflict => Conflict(result),
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpPost(template: "SignIn")]
    public async Task<IActionResult> SignIn([FromBody] UserSignInDTO userSignInDTO)
    {
        ApiResponse result = await _authenticationRepository.SignIn(userSignInDTO);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status401Unauthorized => Unauthorized(result),
            StatusCodes.Status403Forbidden => StatusCode(StatusCodes.Status403Forbidden, result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpPost(template: "ForgetPassword")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO forgetPasswordDTO)
    {
        ApiResponse result = await _authenticationRepository.ForgetPassword(forgetPasswordDTO);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpPost(template: "ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
    {
        ApiResponse result = await _authenticationRepository.ResetPassword(resetPasswordDTO);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => Ok(result),
            StatusCodes.Status400BadRequest => Ok(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }
}