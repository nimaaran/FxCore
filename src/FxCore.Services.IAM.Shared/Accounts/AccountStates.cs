namespace FxCore.Services.IAM.Shared.Accounts;

public enum AccountStates : byte
{
    REGISTERED = 1,
    ACTIVATED = 2,
    DEACTIVATED = 3,
    RESTRICTED = 4,
    CLOSED = 5,
    BANNED = 6,
}
