using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
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
public class AuthenticationController(ApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, EmailService emailService)
 : ControllerBase
{
    private readonly AuthenticationRepository _authenticationRepository = new(applicationDbContext, userManager, roleManager, configuration, emailService);

    [HttpPost(template: "UserSignUp")]
    public async Task<IActionResult> UserSignUp([FromBody] UserSignUpDTO userSignUpDTO)
    {
        ApiResponse result = await _authenticationRepository.UserSignUp(userSignUpDTO);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet(template: "ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        ApiResponse result = await _authenticationRepository.ConfirmEmail(token, email);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "SignIn")]
    public async Task<IActionResult> SignIn([FromBody] UserSignInDTO userSignInDTO)
    {
        ApiResponse result = await _authenticationRepository.SignIn(userSignInDTO);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "ForgetPassword")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO forgetPasswordDTO)
    {
        ApiResponse result = await _authenticationRepository.ForgetPassword(forgetPasswordDTO);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
    {
        ApiResponse result = await _authenticationRepository.ResetPassword(resetPasswordDTO);
        return HelperFunctions.ResponseFormatter(this, result);
    }
}