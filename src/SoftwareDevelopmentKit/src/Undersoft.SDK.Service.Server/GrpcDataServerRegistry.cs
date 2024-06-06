namespace Undersoft.SDK.Service.Server
{
    public static class GrpcDataServerRegistry
    {
        public static ISeries<Type> ServiceContracts = new Registry<Type>();
    }
}
