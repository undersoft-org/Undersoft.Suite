using IdentityModel.Client;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class ApiDataContext : ApiDataClient
    {
        protected ISeries<Arguments> CommandRegistry = new Registry<Arguments>();

        private ISecurityString _securityString;

        public ApiDataContext(Uri serviceUri) : base(serviceUri) { }

        public Task CommandAsync<TEntity>(CommandType command, TEntity payload, string name)
        {
            return Task.Factory.StartNew(() =>
            {
                Command(command, payload, name);
            });
        }

        public Task CommandSetAsync<TEntity>(
            CommandType command,
            IEnumerable<TEntity> payload,
            string name
        )
        {
            return Task.Factory.StartNew(() =>
            {
                CommandSet(command, payload, name);
            });
        }

        public void Command<TEntity>(CommandType command, TEntity payload, string name)
        {
            if (!CommandRegistry.TryGet(typeof(TEntity), out Arguments args))
            {
                args = new Arguments(name);
                args.Add(new DataArgument(name, payload, command.ToString(), typeof(TEntity).Name));
                CommandRegistry.Add(typeof(TEntity), args);
            }
            else
            {
                args.Add(new DataArgument(command.ToString(), payload, name, typeof(TEntity).Name));
            }
        }

        public void CommandSet<TEntity>(
            CommandType command,
            IEnumerable<TEntity> payload,
            string name
        )
        {
            if (!CommandRegistry.TryGet(typeof(TEntity), out Arguments args))
            {
                args = new Arguments(name);
                args.Add(
                    new DataArgument(
                        name,
                        payload.ToArray(),
                        command.ToString(),
                        typeof(TEntity).Name
                    )
                );
                CommandRegistry.Add(typeof(TEntity), args);
            }
            else
            {
                args.Add(new DataArgument(command.ToString(), payload, name, typeof(TEntity).Name));
            }
        }

        protected async Task<HttpResponseMessage[]> CommandSetHandler()
        {
            return await Task.WhenAll(
                CommandRegistry.SelectMany(
                    (cmd) =>
                    {
                        return cmd.GroupBy(m => m.MethodName)
                            .ForEach(
                                async (method) =>
                                {
                                    return await SendCommand(
                                        method.Key,
                                        cmd.TargetName,
                                        new DataContent(
                                            method.Select(a => ((DataArgument)a)).ToArray()
                                        )
                                    );
                                }
                            );
                    }
                )
            );
        }

        protected async Task<HttpResponseMessage[]> CommandHandler()
        {
            return await Task.WhenAll(
                CommandRegistry.SelectMany(
                    (cmd) =>
                    {
                        return cmd.GroupBy(m => m.MethodName)
                            .SelectMany(
                                (method) =>
                                    method.ForEach(
                                        async (arg) =>
                                        {
                                            return await SendCommand(
                                                method.Key,
                                                $"{cmd.TargetName}/{arg.DataKey}",
                                                new DataContent(arg)
                                            );
                                        }
                                    )
                            );
                    }
                )
            );
        }

        protected Task<HttpResponseMessage> SendCommand(
            string command,
            string name,
            DataContent content
        )
        {
            if (_securityString != null)
                this.SetBearerToken(_securityString.Encoded);

            if (command == "POST")
                return PostAsync(name, content);
            if (command == "PATCH")
                return PatchAsync(name, content);
            if (command == "PUT")
                return PutAsync(name, content);
            if (command == "DELETE")
                return DeleteAsync(name);
            return default;
        }

        public Task<HttpResponseMessage[]> CommitExecution(bool transaction)
        {
            if (transaction)
                return CommandSetHandler();
            else
                return CommandHandler();
        }
    }

    public enum CommandType
    {
        POST,
        PATCH,
        PUT,
        DELETE
    }
}
