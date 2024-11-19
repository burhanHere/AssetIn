using System.Net;

namespace AssetIn.Server.Helpers;

public class HelperFunctions
{
    public static string TokenToLink(string baseUrl, string endpoint, string token, string email)
    {
        // URL encoding the token and email address
        var encodedToken = WebUtility.UrlEncode(token);
        var encodedEmail = WebUtility.UrlEncode(email);

        var emailConfirmationLink = $"{baseUrl}/{endpoint}?token={encodedToken}&email={encodedEmail}";
        return emailConfirmationLink;
    }
}
