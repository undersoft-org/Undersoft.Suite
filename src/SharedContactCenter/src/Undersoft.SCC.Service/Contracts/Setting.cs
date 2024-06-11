// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using System.Runtime.Serialization;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SCC.Service.Contracts
{
    /// <summary>
    /// The setting.
    /// </summary>
    [DataContract]
    public class Setting : ObjectSetting<Setting, SettingKind>, ISetting, IContract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        public Setting() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="kind">The kind.</param>
        public Setting(SettingKind kind) : base(kind) { }
    }
}
