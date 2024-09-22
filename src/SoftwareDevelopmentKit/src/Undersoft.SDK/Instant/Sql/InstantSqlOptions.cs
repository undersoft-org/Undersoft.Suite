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

    public class InstantSqlOptions
    {
        public string Database { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public SqlProvider Provider { get; set; }
        public bool Security { get; set; }
        public string Server { get; set; }
        public string UserId { get; set; }

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
