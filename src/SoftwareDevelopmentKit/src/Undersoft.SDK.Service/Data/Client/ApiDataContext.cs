using IdentityModel.Client;
using System.Net;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class ApiDataContext : ApiDataClient
    {
        private readonly Registry<Arguments> CommandRegistry;

        public ApiDataContext(Uri serviceUri) : base(serviceUri)
        {
            CommandRegistry = new Registry<Arguments>(true);
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
                args.Put(new DataArgument(command.ToString(), payload, name, typeof(TEntity).Name));
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
                args.Put(new DataArgument(command.ToString(), payload, name, typeof(TEntity).Name));
            }
        }

        protected async Task<string[]> CommandSetHandler()
        {
            var messages = new Registry<string>();

            await using (CommandRegistry)
            {

                foreach (var cmdType in CommandRegistry)
                {
                    foreach (var cmdMethod in cmdType.Value.GroupBy(method => method.MethodName))
                    {
                        var args = cmdMethod.Select(arg => (DataArgument)arg).ToArray();
                        var response = await CommandRequest(
                                                        cmdMethod.Key,
                                                        cmdType.Value.TargetName,
                                                        new DataContent(args)
                                                    );

                        var message = await GetResponseMessage(response);

                        messages.Add(message);
                    }
                }
            }

            return messages.ToArray();
        }

        protected async Task<string[]> CommandHandler()
        {
            var messages = new List<string>();

            await using (CommandRegistry)
            {
                foreach (var cmdType in CommandRegistry)
                {
                    foreach (var cmdMethod in cmdType.Value.GroupBy(method => method.MethodName))
                    {
                        foreach (var arg in cmdMethod)
                        {
                            var response = await CommandRequest(
                                                            cmdMethod.Key,
                                                            cmdType.Value.TargetName,
                                                            new DataContent((DataArgument)arg)
                                                        );

                            var message = await GetResponseMessage(response);

                            messages.Add(message);
                        }
                    }
                }
            }

            return messages.ToArray();
        }

        protected async Task<HttpResponseMessage> CommandRequest(
            string command,
            string name,
            DataContent content
        )
        {
            if (Enum.TryParse<CommandType>(command, out CommandType commandEnum))
            {
                switch (commandEnum)
                {
                    case CommandType.POST:
                        return await PostAsync(name, content);
                    case CommandType.PATCH:
                        return await PatchAsync(name, content);
                    case CommandType.DELETE:
                        return await DeleteAsync(name, content);
                    case CommandType.PUT:
                        return await PutAsync(name, content);
                    default:
                        break;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Unsupported commend")
            };
        }

        public async Task<string[]> SendCommands(bool changesets)
        {
            if (!HasCommands)
                return default;

            if (changesets)
                return await CommandSetHandler();
            else
                return await CommandHandler();
        }

        private Registry<Arguments> GetProcessRegistry()
        {
            var processRegistry = new Registry<Arguments>();
            CommandRegistry.ForEach(typeArgs =>
            {
                var processArgs = new Arguments(typeArgs.TargetType);
                processArgs.Add(typeArgs.ForEach(arg => typeArgs.Remove(arg.Id)).Commit());
                processRegistry.Add(typeArgs.TargetType, processArgs);
            });
            return processRegistry;
        }

        private async Task<string> GetResponseMessage(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var message = $"StatusCode:{response.StatusCode.ToString()} Message:{content}";
            if ((int)response.StatusCode > 210)
                this.Info<Apilog>(message);
            else
                this.Info<Apilog>(message);
            return message;
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
