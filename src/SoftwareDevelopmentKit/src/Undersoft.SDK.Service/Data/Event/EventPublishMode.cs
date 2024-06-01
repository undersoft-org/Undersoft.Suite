using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Undersoft.SDK.Service.Data.Event
{
    public enum EventPublishMode
    {
        PropagateCommand,
        SuppressCommand
    }
}