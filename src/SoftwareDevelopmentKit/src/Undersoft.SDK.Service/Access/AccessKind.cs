using System;
using System.Collections.Generic;

namespace Undersoft.SDK.Service.Access
{
    public enum AccessKind
    {
        None,
        SignIn,
        SignUp,
        SignOut,
        SetPassword,
        SetEmail,
        ConfirmPassword,
        ConfirmEmail,
        CompleteRegistration,
        Renew
    }
}