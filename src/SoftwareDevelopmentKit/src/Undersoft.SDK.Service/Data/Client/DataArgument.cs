namespace Undersoft.SDK.Service.Data.Client;

public class DataArgument : Argument
{
    public DataArgument(string name, object value, string method, string target = null)
    {
        Set(name, value, method, target);
    }

    public object DataObject { get; set; }

    public override void Set(
        string name,
        object dataObject,
        string method,
        string target = null,
        int position = 0
    )
    {
        Name = name;
        _type = dataObject.GetType();

        if (_type.IsAssignableTo(typeof(IIdentifiable)))
            DataKey = ((IIdentifiable)dataObject).Id;

        ArgumentTypeName = _type.FullName;
        DataObject = dataObject;
        Position = position;
        MethodName = method;
        TargetName = target;
        TypeId = ArgumentTypeName.UniqueKey();
        Id = DataKey.UniqueKey64(MethodName.UniqueKey64());

    }
}
