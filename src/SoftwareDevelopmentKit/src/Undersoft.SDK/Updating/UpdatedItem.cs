// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SDK
// *************************************************


namespace Undersoft.SDK.Updating
{
    public struct UpdatedItem
    {
        public int TargetIndex;
        public object OriginValue;
        public object TargetValue;
        public Type OriginType;
        public Type TargetType;
    }
}
