// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Types;

/// <summary>
/// Defines a list from default and general result codes.
/// </summary>
public enum ResultCodes : short
{
    /// <summary>
    /// Like HTTP 200, it means that the request has succeeded.
    /// </summary>
    OK = 200,

    /// <summary>
    /// Like HTTP 304, it means that there was no new data to return or the server state has not
    /// changed.
    /// </summary>
    NOT_MODIFIED = 304,

    /// <summary>
    /// Like HTTP 400, it means that the server could not understand the request due to invalid
    /// parameters and arguments.
    /// </summary>
    BAD_REQUEST = 400,

    /// <summary>
    /// Like HTTP 401, it means that the request requires user authentication.
    /// </summary>
    AUTHENTICATION_REQUIRED = 401,

    /// <summary>
    /// Like HTTP 403, it means that the server understood the request, but refuses to authorize
    /// it due to insufficient user permissions.
    /// </summary>
    AUTHORIZATION_REQUIRED = 403,

    /// <summary>
    /// Like HTTP 404, it means that the requested resource could not be found on the server.
    /// </summary>
    NOT_FOUND = 404,

    /// <summary>
    /// Like HTTP 409, it means that the request could not be completed due to a conflict with the
    /// existing resources or state of the server.
    /// </summary>
    INCONSISTENCY = 409,

    /// <summary>
    /// Like HTTP 410, it means that the requested resource is no longer available and has been
    /// removed or archived.
    /// </summary>
    ARCHIVED = 410,

    /// <summary>
    /// Like HTTP 500, it means that the server encountered an unexpected condition that prevented
    /// it to respond to the request.
    /// </summary>
    SERVER_ERROR = 500,
}
