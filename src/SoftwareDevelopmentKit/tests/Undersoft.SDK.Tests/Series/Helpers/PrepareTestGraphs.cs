namespace System.Series.Tests
{
    using Undersoft.SDK;
    using Undersoft.SDK.Series.Complex;

    public static class PrepareTestGraphs
    {
        public static Place<T>[] preparePlaces<T>() where T : class, IIdentifiable
        {
            return new Place<T>[0];
        }
    }
}
