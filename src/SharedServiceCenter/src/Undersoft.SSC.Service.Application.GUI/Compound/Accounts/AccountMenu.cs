using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Accounts;

public class AccountMenu : DataObject
{
    [MenuGroup]
    [Extended]
    public AccountMenuItems Account { get; set; } = new AccountMenuItems();
}

