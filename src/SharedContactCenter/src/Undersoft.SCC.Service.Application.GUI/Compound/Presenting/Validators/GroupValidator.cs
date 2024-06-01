﻿using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Access;

public class GroupValidator : ViewValidator<Group>
{
    public GroupValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Model.Name!);
            }
        );

        //ValidationScope(
        //    OperationType.Create,
        //    () =>
        //    {
        //        ValidateNotExist<IDataStore, Group>(c => p => p.Name!.Equals(c.Name));
        //    }
        //);

        //ValidationScope(
        //    OperationType.Delete | OperationType.Update,
        //    () =>
        //    {
        //        ValidateExist<IDataStore, Group>(c => p => p.Name!.Equals(c.Name));
        //    }
        //);
    }
}
