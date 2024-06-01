namespace Undersoft.SDK.Service.Hosting
{
    public class SslCertificate
    {
        public int Port { get; set; }

        public string Protocols { get; set; }

        public string Path { get; set; }

        public string KeyPath { get; set; }

        public string Password { get; set; }
    }
}