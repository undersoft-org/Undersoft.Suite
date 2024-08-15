// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.GDC.Service
// *************************************************

using System.Runtime.Serialization;
using Undersoft.SDK.Service.Access.Identity;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.GDC.Service.Contracts.Accounts
{
    [DataContract]
    public class AccountUser : User, IContract
    {
    }
}
