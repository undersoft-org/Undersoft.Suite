using Undersoft.SCC.Service.Application.GUI.Compound.Access;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Accounts;

public class AccountMenuItems : DataObject
{
    [MenuItem]
    [Extended]
    [DisplayRubric("Account")]
    [Invoke(typeof(AccountPanel), "Open")]
    public IViewPanel<Account> Account { get; set; } = default!;

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Sign up")]
    public string SignUp { get; set; } = "/access/sign_up";

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Sign in")]
    public string SignIn { get; set; } = "/access/sign_in";

    [Link]
    [MenuItem]
    [Extended]
    [DisplayRubric("Sign out")]
    public string SignOut { get; set; } = "/access/sign_out";
}

