using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
using AssetIn.Server.Models;
using AssetIn.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssetIn.Server.Repositories;

public class AuthenticationRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, EmailService emailService)
{
  private readonly UserManager<User> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;
  private readonly IConfiguration _configuration = configuration;
  private readonly EmailService _emailService = emailService;
  private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

  public async Task<ApiResponse> UserSignUp(UserSignUpDTO userSignUpDTO)
  {
    // check is user already exist with this email
    var userEmailExist = await _userManager.FindByEmailAsync(userSignUpDTO.Email!);
    if (userEmailExist != null)
    {
      return new()
      {
        Status = StatusCodes.Status409Conflict,
        ResponseData = new List<string> { "User email already registered." },
        Errors = null,
      };
    }

    //creating new user record
    User newUser = new()
    {
      Email = userSignUpDTO.Email,
      UserName = userSignUpDTO.UserName,
      SecurityStamp = Guid.NewGuid().ToString(),
    };

    var createNewUser = await _userManager.CreateAsync(newUser, userSignUpDTO.Password!);
    if (!createNewUser.Succeeded)
    {
      //if fail to create new user
      return new()
      {
        Status = StatusCodes.Status400BadRequest,
        ResponseData = new List<string>
                {"Unable to create new user Account"},
        Errors = createNewUser.Errors,
      };
    }
    //check if the required role exist in db
    var requiredRoleExist = await _roleManager.FindByNameAsync(userSignUpDTO.RequiredRole!);

    if (requiredRoleExist == null && (requiredRoleExist?.NormalizedName != "ORGANIZATIONOWNER" || requiredRoleExist.NormalizedName != "VENDOR"))
    {
      //if the required role does not exist
      var deleteUser = await _userManager.DeleteAsync(newUser);
      return new()
      {
        Status = StatusCodes.Status400BadRequest,
        ResponseData = new List<string> { "Unable to create new user Account." },
        Errors = null,
      };
    }
    // assigning the required role to the user
    var newUserRole = await _userManager.AddToRoleAsync(newUser, requiredRoleExist.Name!);
    if (!newUserRole.Succeeded)
    {
      // if fail to assign the required role to the user
      var deleteUser = await _userManager.DeleteAsync(newUser);
      return new()
      {
        Status = StatusCodes.Status400BadRequest,
        ResponseData = new List<string> { "Unable to create new user Account." },
        Errors = null,
      };
    }
    // genrating email confirmation token
    var targetUser = await _userManager.FindByEmailAsync(userSignUpDTO.Email!);
    var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(targetUser!);
    var tokenToLink = HelperFunctions.TokenToLink(_configuration.GetValue<string>("JWT:ValidAudience") + "//auth", "EmailConfirmation", emailConfirmationToken, newUser.Email!);

    //Confirmation Email message 
    string message = $"<h2>Hello,</h2>\nPlease click the below link to confirm you email address.\n Confirmation Link: <a href={tokenToLink}>Click Here </a>";
    //Confirmation Email subject 
    string subject = "AssetIn: Confirmation E-Mail (No Reply)";
    //Sending Conformation Email 
    var confirmationEmailSent = await _emailService.SendEmailAsync(newUser.Email!, subject, message);
    if (!confirmationEmailSent)
    {
      // Account created, but email sending failed
      return new()
      {
        Status = StatusCodes.Status200OK,
        ResponseData = new List<string>
                    {
                        "Account created successfully. LogIn to get email confirmation link."
                    },
        Errors = null,
      };
    }

    // Account created, Email sent and role assigned successfully 
    return new()
    {
      Status = StatusCodes.Status200OK,
      ResponseData = new List<string> { "Account created successfully.", "Confirmation email sent." }
    };
  }

  public async Task<ApiResponse> SignIn(UserSignInDTO userSignInDTO)
  {
    // check is user exists
    var userExist = await _userManager.FindByEmailAsync(userSignInDTO.Email!);
    if (userExist == null)
    {
      return new()
      {
        Status = StatusCodes.Status404NotFound,
        ResponseData = new List<string>() { "User Not Found" },
        Errors = null,
      };
    }
    if (!userExist.Status)
    {
      return new()
      {
        Status = StatusCodes.Status403Forbidden,
        ResponseData = new List<string> { "User account is blocked. Contact Admin Sopport." },
        Errors = null,
      };
    }
    // check is user email is confirmed or not
    if (!userExist.EmailConfirmed)
    {
      // if not email confirend 
      // genrating email confirmation token
      var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userExist);
      //Confirmation Email message 
      string message = $"Please click the below link to confirm you email address.\n Confirmation Link: <a href={HelperFunctions.TokenToLink(_configuration.GetValue<string>("JWT:ValidAudience") + "/auth", "emailConfirmation", emailConfirmationToken, userExist.Email!)}>Click Here </a>";
      //Confirmation Email subject 
      string subject = "Confirmation E-Mail (No Reply)";
      //Sending Conformation Email 
      bool confirmationEmailSent = await _emailService.SendEmailAsync(userExist.Email!, subject, message);
      if (!confirmationEmailSent)
      {
        // Account created, but email sending failed
        return new()
        {
          Status = StatusCodes.Status403Forbidden,
          ResponseData = new List<string>
                    {
                        "Unable to send confirmation email. Please try to log in later to confirm your email."
                    },
          Errors = "Email not confirmed"
        };
      }
      // user can login because email not confirmed
      return new()
      {
        Status = StatusCodes.Status403Forbidden,
        ResponseData = new List<string>{
                    "Your email address has not been confirmed. Please check your inbox for the confirmation email and verify your account by the link in the email."
                    },
        Errors = "Email not confirmed"
      };
    }
    // varify user password
    bool correctPassowrd = await _userManager.CheckPasswordAsync(userExist, userSignInDTO.Password!);
    if (!correctPassowrd)
    {
      return new()
      {
        Status = StatusCodes.Status401Unauthorized,
        ResponseData = new List<string> { "Invalid Email or Password" },
        Errors = null,
      };
    }
    // adding user Claims which we want to send in jwt token
    var authClaims = new List<Claim>
        {
            new("UserId", userExist.Id),
            new("Email", userExist.Email!),
            new("EmailConfirmed", userExist.EmailConfirmed?"true":"flase"),
            new("FullName",userExist.UserName!),
              new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        };
    var userRoles = await _userManager.GetRolesAsync(userExist);
    authClaims.AddRange(userRoles.Select(role => new Claim("Role", role)));
    if (userRoles[0] == "OrganizationAssetManager" || userRoles[0] == "OrganizationEmployee")
    {
      authClaims.Add(new Claim("OrganizationId", userExist.OrganizationId.ToString()!));
    }
    else if (userRoles[0] == "Vendor")
    {
      var targetVendorData = _applicationDbContext.Vendors.FirstOrDefault(x => userExist.Id == x.UserID);
      if (targetVendorData == null)
      {
        authClaims.Add(new Claim("VendorId", 0.ToString()));
      }
      else
      {
        authClaims.Add(new Claim("VendorId", targetVendorData?.VendorID.ToString()!));
      }
    }
    // getting encryption key for jwt token encruption
    var secret = _configuration.GetValue<string>("JWT:Secret");
    // getting Valid Issuer of the token
    var issuer = _configuration.GetValue<string>("JWT:ValidIssuer");
    // setting up encryption key to encrypt jwt
    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
    // formeing jwt token
    var newJwtToken = new JwtSecurityToken(
        issuer: issuer,
        expires: DateTime.Now.AddHours(12),
        claims: authClaims,
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
    );
    // genrating final jwt token for the user authorizatin
    var finalJwtToken = new JwtSecurityTokenHandler().WriteToken(newJwtToken);
    return new()
    {
      Status = StatusCodes.Status200OK,
      ResponseData = new
      {
        message = "SignIn Successful",
        jwt = finalJwtToken
      }
    };
  }

  public async Task<ApiResponse> ConfirmEmail(string token, string email)
  {
    // check is user exists
    var userExist = await _userManager.FindByEmailAsync(email!);
    if (userExist == null)
    {
      return new()
      {
        Status = StatusCodes.Status404NotFound,
        ResponseData = new List<string>
                {
                    "User Not Found"
                },
        Errors = null,
      };
    }
    if (!userExist.Status)
    {
      return new()
      {
        Status = StatusCodes.Status403Forbidden,
        ResponseData = new List<string> { "User account is blocked. Contact Admin Sopport." },
        Errors = null,
      };
    }
    // check is user email is confirmed or not
    if (userExist.EmailConfirmed)
    {
      // User already confirmed
      return new()
      {
        Status = StatusCodes.Status409Conflict,
        ResponseData = new List<string>
                {
                    "Your email address is already confirmed. Try to login."
                },
        Errors = null
      };
    }
    // confirming the email
    var confirmationResult = _userManager.ConfirmEmailAsync(userExist, token);
    if (!confirmationResult.Result.Succeeded)
    {
      return new()
      {
        Status = StatusCodes.Status400BadRequest,
        ResponseData = new List<string>
                {
                    "Failed to confirm email"
                },
        Errors = null
      };
    }
    return new()
    {
      Status = StatusCodes.Status200OK,
      ResponseData = new List<string>
            {
                "Email address is confirmed successfully."
            },
      Errors = null
    };
  }

  public async Task<ApiResponse> ForgetPassword(ForgetPasswordDTO userSignInDTO)
  {
    // check is user exists
    var userExist = await _userManager.FindByEmailAsync(userSignInDTO.Email!);
    if (userExist == null)
    {
      return new()
      {
        Status = StatusCodes.Status404NotFound,
        ResponseData = new List<string> { "User Not Found" },
        Errors = null,
      };
    }
    if (!userExist.Status)
    {
      return new()
      {
        Status = StatusCodes.Status403Forbidden,
        ResponseData = new List<string> { "User account is blocked. Contact Admin Sopport." },
        Errors = null,
      };
    }
    //genrating Password Reset Token
    var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userExist);
    // converting he token in to link forming messgae 
    string message = $"Please click the link below to reset your password.\nPassowrd Reset Link: <a href ={HelperFunctions.TokenToLink("http://localhost:4200/auth", "resetPassword", passwordResetToken, userExist.Email!)}>Click</a>";
    // subject of passowrd reset email  
    string subject = "Reset password E-Mail (No Reply)";
    bool emailStatus = await _emailService.SendEmailAsync(userExist.Email!, subject, message);
    if (!emailStatus)
    {
      // Failed to send the reset password email
      return new()
      {
        Status = StatusCodes.Status400BadRequest,
        ResponseData = new List<string> { "Unable to send Reset password email. Please try again later." },
        Errors = null
      };
    }
    return new()
    {
      Status = StatusCodes.Status200OK,
      ResponseData = new List<string> { "Passowrd reset link sent to you email. Please check you email." },
      Errors = null
    };
  }

  public async Task<ApiResponse> ResetPassword(ResetPasswordDTO resetPassowrdDTO)
  {
    // check is user exists
    var userExist = await _userManager.FindByEmailAsync(resetPassowrdDTO.Email!);
    if (userExist == null)
    {
      return new()
      {
        Status = StatusCodes.Status404NotFound,
        ResponseData = new List<string> { "User Not Found" },
        Errors = null,
      };
    }
    if (!userExist.Status)
    {
      return new()
      {
        Status = StatusCodes.Status403Forbidden,
        ResponseData = new List<string> { "User account is blocked. Contact Admin Sopport." },
        Errors = null,
      };
    }
    // validate password reset token and reset password
    var passwordReset = _userManager.ResetPasswordAsync(userExist, resetPassowrdDTO.Token!, resetPassowrdDTO.NewPassword!);
    if (!passwordReset.Result.Succeeded)
    {
      return new()
      {
        Status = StatusCodes.Status400BadRequest,
        ResponseData = new List<string> { "Failed to reset password." },
        Errors = null
      };
    }
    return new()
    {
      Status = StatusCodes.Status200OK,
      ResponseData = new List<string> { "Password reset successful. Please log in again." },
      Errors = null
    };
  }
}