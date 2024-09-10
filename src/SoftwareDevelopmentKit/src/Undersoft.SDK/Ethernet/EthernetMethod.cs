using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Ethernet
{
    public class EthernetMethod : Invoker
    {
        public EthernetMethod(string MethodName, object TargetClassObject, params object[] parameters)
            : base(TargetClassObject, MethodName)
        {
            base.Arguments = new Arguments(MethodName, parameters);
        }

        public EthernetMethod(string MethodName, string TargetClassName, params object[] parameters)
            : base(TargetClassName, MethodName)
        {
            base.Arguments = new Arguments(MethodName, parameters);
        }
    }
}
