namespace FxCore.Abstraction.Types;

public enum ResultCodes : short
{
    OK = 200,
    NOT_MODIFIED = 304,
    BAD_REQUEST = 400,
    AUTHENTICATION_REQUIRED = 401,
    AUTHORIZATION_REQUIRED = 403,
    NOT_FOUND = 404,
    INCONSISTENCY = 409,
    ARCHIVED = 410,
    SERVER_ERROR = 500,
}
