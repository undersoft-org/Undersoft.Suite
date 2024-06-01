namespace System
{
    public enum SqlProvider
    {
        MsSql,
        MySql,
        Postgres,
        Oracle,
        SqlLite
    }

    [Serializable]
    public class InstantSqlOptions
    {
        public string AuthId;
        public string Database;
        public int Id;
        public string Name;
        public string Password;
        public int Port;
        public SqlProvider Provider;
        public bool Security;
        public string Server;
        public string UserId;

        public InstantSqlOptions() { }

        public InstantSqlOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString
        {
            get
            {
                string cn = string.Format(
                    "server={0}{1};Persist Security Info={2};password={3};User ID={4};database={5}",
                    Server,
                    (Port != 0) ? ":" + Port.ToString() : "",
                    Security.ToString(),
                    Password,
                    UserId,
                    Database
                );
                return cn;
            }
            set
            {
                string cn = value;
                string[] opts = cn.Split(';');
                foreach (string opt in opts)
                {
                    string name = opt.Split('=')[0].ToLower();
                    string val = opt.Split('=')[1];
                    switch (name)
                    {
                        case "server":
                            Server = val;
                            break;
                        case "persist security info":
                            Security = Boolean.Parse(val);
                            break;
                        case "password":
                            Password = val;
                            break;
                        case "user id":
                            UserId = val;
                            break;
                        case "database":
                            Database = val;
                            break;
                    }
                }
            }
        }
    }
}
