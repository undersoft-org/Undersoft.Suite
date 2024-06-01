using System;

namespace System.Net;

public static class InternalIPAddressExtensions
{
    public static string ToIPv4String(this IPAddress address)
    {
        var ipv4Address = (address ?? IPAddress.IPv6Loopback).ToString();
        return ipv4Address.StartsWith("::ffff:") ? (address ?? IPAddress.IPv6Loopback).MapToIPv4().ToString() : ipv4Address;
    }
}
