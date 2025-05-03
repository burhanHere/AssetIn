using System.Net;
using AssetIn.Server.DTOs;
using Microsoft.AspNetCore.Mvc;

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

    public static IActionResult ResponseFormatter(ControllerBase controller, ApiResponse result)
    {
        return result.Status switch
        {
            StatusCodes.Status100Continue => controller.StatusCode(StatusCodes.Status100Continue, result),
            StatusCodes.Status101SwitchingProtocols => controller.StatusCode(StatusCodes.Status101SwitchingProtocols, result),
            StatusCodes.Status102Processing => controller.StatusCode(StatusCodes.Status102Processing, result),
            StatusCodes.Status200OK => controller.Ok(result),
            StatusCodes.Status201Created => controller.StatusCode(StatusCodes.Status201Created, result),
            StatusCodes.Status202Accepted => controller.StatusCode(StatusCodes.Status202Accepted, result),
            StatusCodes.Status203NonAuthoritative => controller.StatusCode(StatusCodes.Status203NonAuthoritative, result),
            StatusCodes.Status204NoContent => controller.StatusCode(StatusCodes.Status204NoContent, result),
            StatusCodes.Status205ResetContent => controller.StatusCode(StatusCodes.Status205ResetContent, result),
            StatusCodes.Status206PartialContent => controller.StatusCode(StatusCodes.Status206PartialContent, result),
            StatusCodes.Status207MultiStatus => controller.StatusCode(StatusCodes.Status207MultiStatus, result),
            StatusCodes.Status208AlreadyReported => controller.StatusCode(StatusCodes.Status208AlreadyReported, result),
            StatusCodes.Status226IMUsed => controller.StatusCode(StatusCodes.Status226IMUsed, result),
            StatusCodes.Status300MultipleChoices => controller.StatusCode(StatusCodes.Status300MultipleChoices, result),
            StatusCodes.Status301MovedPermanently => controller.StatusCode(StatusCodes.Status301MovedPermanently, result),
            StatusCodes.Status302Found => controller.StatusCode(StatusCodes.Status301MovedPermanently, result),
            StatusCodes.Status303SeeOther => controller.StatusCode(StatusCodes.Status303SeeOther, result),
            StatusCodes.Status304NotModified => controller.StatusCode(StatusCodes.Status304NotModified, result),
            StatusCodes.Status305UseProxy => controller.StatusCode(StatusCodes.Status305UseProxy, result),
            StatusCodes.Status306SwitchProxy => controller.StatusCode(StatusCodes.Status306SwitchProxy, result),
            StatusCodes.Status307TemporaryRedirect => controller.StatusCode(StatusCodes.Status307TemporaryRedirect, result),
            StatusCodes.Status308PermanentRedirect => controller.StatusCode(StatusCodes.Status308PermanentRedirect, result),
            StatusCodes.Status400BadRequest => controller.BadRequest(result),
            StatusCodes.Status401Unauthorized => controller.Unauthorized(result),
            StatusCodes.Status402PaymentRequired => controller.StatusCode(StatusCodes.Status402PaymentRequired, result),
            StatusCodes.Status403Forbidden => controller.StatusCode(StatusCodes.Status403Forbidden, result),
            StatusCodes.Status404NotFound => controller.NotFound(result),
            StatusCodes.Status405MethodNotAllowed => controller.StatusCode(StatusCodes.Status405MethodNotAllowed, result),
            StatusCodes.Status406NotAcceptable => controller.StatusCode(StatusCodes.Status406NotAcceptable, result),
            StatusCodes.Status407ProxyAuthenticationRequired => controller.StatusCode(StatusCodes.Status407ProxyAuthenticationRequired, result),
            StatusCodes.Status408RequestTimeout => controller.StatusCode(StatusCodes.Status408RequestTimeout, result),
            StatusCodes.Status409Conflict => controller.Conflict(result),
            StatusCodes.Status410Gone => controller.StatusCode(StatusCodes.Status410Gone, result),
            StatusCodes.Status411LengthRequired => controller.StatusCode(StatusCodes.Status411LengthRequired, result),
            StatusCodes.Status412PreconditionFailed => controller.StatusCode(StatusCodes.Status412PreconditionFailed, result),
            StatusCodes.Status413RequestEntityTooLarge => controller.StatusCode(StatusCodes.Status413RequestEntityTooLarge, result),
            // StatusCodes.Status413PayloadTooLarge => controller.StatusCode(StatusCodes.Status413PayloadTooLarge, result),
            StatusCodes.Status414RequestUriTooLong => controller.StatusCode(StatusCodes.Status414RequestUriTooLong, result),
            // StatusCodes.Status414UriTooLong => controller.StatusCode(StatusCodes.Status414UriTooLong, result),
            StatusCodes.Status415UnsupportedMediaType => controller.StatusCode(StatusCodes.Status415UnsupportedMediaType, result),
            StatusCodes.Status416RequestedRangeNotSatisfiable => controller.StatusCode(StatusCodes.Status416RequestedRangeNotSatisfiable, result),
            // StatusCodes.Status416RangeNotSatisfiable => controller.StatusCode(StatusCodes.Status416RangeNotSatisfiable, result),
            StatusCodes.Status417ExpectationFailed => controller.StatusCode(StatusCodes.Status417ExpectationFailed, result),
            StatusCodes.Status418ImATeapot => controller.StatusCode(StatusCodes.Status418ImATeapot, result),
            StatusCodes.Status419AuthenticationTimeout => controller.StatusCode(StatusCodes.Status419AuthenticationTimeout, result),
            StatusCodes.Status421MisdirectedRequest => controller.StatusCode(StatusCodes.Status421MisdirectedRequest, result),
            StatusCodes.Status422UnprocessableEntity => controller.StatusCode(StatusCodes.Status422UnprocessableEntity, result),
            StatusCodes.Status423Locked => controller.StatusCode(StatusCodes.Status423Locked, result),
            StatusCodes.Status424FailedDependency => controller.StatusCode(StatusCodes.Status424FailedDependency, result),
            StatusCodes.Status426UpgradeRequired => controller.StatusCode(StatusCodes.Status426UpgradeRequired, result),
            StatusCodes.Status428PreconditionRequired => controller.StatusCode(StatusCodes.Status428PreconditionRequired, result),
            StatusCodes.Status429TooManyRequests => controller.StatusCode(StatusCodes.Status429TooManyRequests, result),
            StatusCodes.Status431RequestHeaderFieldsTooLarge => controller.StatusCode(StatusCodes.Status431RequestHeaderFieldsTooLarge, result),
            StatusCodes.Status451UnavailableForLegalReasons => controller.StatusCode(StatusCodes.Status451UnavailableForLegalReasons, result),
            StatusCodes.Status499ClientClosedRequest => controller.StatusCode(StatusCodes.Status499ClientClosedRequest, result),
            StatusCodes.Status500InternalServerError => controller.StatusCode(StatusCodes.Status500InternalServerError, result),
            StatusCodes.Status501NotImplemented => controller.StatusCode(StatusCodes.Status501NotImplemented, result),
            StatusCodes.Status502BadGateway => controller.StatusCode(StatusCodes.Status502BadGateway, result),
            StatusCodes.Status503ServiceUnavailable => controller.StatusCode(StatusCodes.Status503ServiceUnavailable, result),
            StatusCodes.Status504GatewayTimeout => controller.StatusCode(StatusCodes.Status504GatewayTimeout, result),
            StatusCodes.Status505HttpVersionNotsupported => controller.StatusCode(StatusCodes.Status505HttpVersionNotsupported, result),
            StatusCodes.Status506VariantAlsoNegotiates => controller.StatusCode(StatusCodes.Status506VariantAlsoNegotiates, result),
            StatusCodes.Status507InsufficientStorage => controller.StatusCode(StatusCodes.Status507InsufficientStorage, result),
            StatusCodes.Status508LoopDetected => controller.StatusCode(StatusCodes.Status508LoopDetected, result),
            StatusCodes.Status510NotExtended => controller.StatusCode(StatusCodes.Status510NotExtended, result),
            StatusCodes.Status511NetworkAuthenticationRequired => controller.StatusCode(StatusCodes.Status511NetworkAuthenticationRequired, result),
            _ => controller.BadRequest("Not a valid response Code.")
        };
    }
}
