using IdentityModel.Client;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class ApiDataContext : ApiDataClient
    {
        private readonly ISeries<Arguments> CommandRegistry;

        private ISecurityString _securityString;

        public ApiDataContext(Uri serviceUri) : base(serviceUri)
        {
            CommandRegistry = new Registry<Arguments>();
        }

        public bool HasCommands => CommandRegistry.Count > 0;

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
                                    return await CommandRequest(
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
                                            return await CommandRequest(
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

        protected async Task<HttpResponseMessage> CommandRequest(
            string command,
            string name,
            DataContent content
        )
        {
            if (_securityString != null)
                this.SetBearerToken(_securityString.Encoded);

            if (Enum.TryParse<CommandType>(command, out CommandType commandEnum))
            {
                switch (commandEnum)
                {
                    case CommandType.POST: return await PostAsync(name, content);
                    case CommandType.PATCH: return await PatchAsync(name, content);
                    case CommandType.DELETE: return await DeleteAsync(name);
                    case CommandType.PUT: return await PutAsync(name, content);
                    default: break;
                }
            }
            return default;
        }

        public async Task<Task<string>[]> SendCommands(bool changesets)
        {
            if (!HasCommands)
                return default;

            if (changesets)
                return (await CommandSetHandler()).Select(async m =>
                {
                    var contentString = await m.Content.ReadAsStringAsync();
                    if ((int)m.StatusCode > 210)
                        this.Warning<Weblog>(contentString);
                    return contentString;
                }).ToArray();
            else
            {
                return (await CommandHandler()).Select(async m =>
                    {
                        var contentString = await m.Content.ReadAsStringAsync();
                        if ((int)m.StatusCode > 210)
                            this.Warning<Weblog>(contentString);
                        return contentString;
                    }).ToArray();
            }
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
