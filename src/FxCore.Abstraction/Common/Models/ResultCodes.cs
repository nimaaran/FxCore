// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Common.Models;

/// <summary>
/// Defines a list of general result codes.
/// </summary>
public enum ResultCodes : short
{
    /// <summary>
    /// It means the operation has completed successfully and you can get the outcome.
    /// </summary>
    OK = 200,

    /// <summary>
    /// It means the operation has completed successfully but has not generated any outcome.
    /// </summary>
    NO_CONTENT = 204,

    /// <summary>
    /// It means the operation has terminated and has not changed the state of the system.
    /// </summary>
    NOT_MODIFIED = 304,

    /// <summary>
    /// It means the operation has terminated because the request is not in a correct format.
    /// </summary>
    BAD_REQUEST = 400,

    /// <summary>
    /// It means the operation has terminated because the asker should be authenticated or
    /// authorized.
    /// </summary>
    UNAUTHORIZED = 401,

    /// <summary>
    /// It means the operation has terminated because the request forbidden.
    /// </summary>
    FORBIDDEN = 403,

    /// <summary>
    /// It means the operation has terminated because desired resources are not available.
    /// </summary>
    NOT_FOUND = 404,

    /// <summary>
    /// It means the operation has terminated because it causes an inconsistency or conflict in the
    /// system.
    /// </summary>
    CONFLICT = 409,

    /// <summary>
    /// It means the operation has terminated because desired objects were removed or archived.
    /// </summary>
    ARCHIVED = 410,

    /// <summary>
    /// It means the operation has failed unexpectedly.
    /// </summary>
    SERVER_ERROR = 500,
}
