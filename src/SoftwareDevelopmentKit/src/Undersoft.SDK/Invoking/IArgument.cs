using Undersoft.SDK.Series;

namespace Undersoft.SDK.Invoking
{
    public interface IArgument : IIdentifiable
    {
        string Name { get; set; }
        byte[] Data { get; set; }
        string TypeName { get; set; }
        string TargetName { get; set; }
        string ArgumentTypeName {  get; set; }
        string MethodName { get; set; } 
        int Position { get; set; }
    }
}