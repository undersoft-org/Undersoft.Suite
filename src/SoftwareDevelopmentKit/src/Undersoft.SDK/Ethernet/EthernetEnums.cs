namespace Undersoft.SDK.Ethernet
{
    using System;

    [Serializable]
    public enum DirectionType
    {
        Send,
        Receive,
        None
    }

    [Serializable]
    public enum EthernetProtocol
    {
        NONE,
        DOTP,
        HTTP
    }

    [Serializable]
    public enum ProtocolMethod
    {
        NONE,
        DATA,
        SYNC,
        GET,
        POST,
        OPTIONS,
        PUT,
        DELETE,
        PATCH
    }

    [Serializable]
    public enum TransitComplexity
    {
        Guide,
        Basic,
        Standard,
        Advanced
    }

    [Serializable]
    public enum TransitPart
    {
        Header,
        Message,
    }
}
