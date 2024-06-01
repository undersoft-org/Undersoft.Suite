namespace Undersoft.SDK.Plugging
{
    using System.Reflection;
    using System.Runtime.Loader;

    public class Plugin : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;
        private string _assemblyPath;
        private string _symbolsPath;

        public Plugin() : this(AppDomain.CurrentDomain.BaseDirectory)
        {
        }

        public Plugin(string assemblyPath, string symbolsPath) : base(Path.GetFileNameWithoutExtension(assemblyPath), true)
        {
            _assemblyPath = assemblyPath;
            _symbolsPath = symbolsPath;
        }

        public Plugin(string assemblyPath) : this(assemblyPath, null)
        {
            _resolver = new AssemblyDependencyResolver(assemblyPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }

        public Assembly Load(string assemblyPath, string symbolsPath = null)
        {
            byte[] dllBytes;
            if (assemblyPath != null)
            {
                dllBytes = File.ReadAllBytes(_assemblyPath);
                byte[] pdbBytes;
                if (symbolsPath != null)
                {
                    pdbBytes = File.ReadAllBytes(_symbolsPath);
                    return Load(dllBytes, pdbBytes);
                }
                return Load(dllBytes);
            }
            return null;
        }

        public Assembly Load(Stream dllStream, Stream pdbStream = null)
        {
            if (dllStream != null)
            {
                if (pdbStream != null)
                {
                    return LoadFromStream(dllStream, pdbStream);
                }
                return LoadFromStream(dllStream);
            }
            return null;
        }

        public Assembly Load(byte[] dllBytes, byte[] pdbBytes = null)
        {
            if (dllBytes != null)
            {
                if (pdbBytes != null)
                {
                    pdbBytes = File.ReadAllBytes(_symbolsPath);
                    return LoadFromStream(new MemoryStream(dllBytes), new MemoryStream(pdbBytes));
                }
                return LoadFromStream(new MemoryStream(dllBytes));
            }
            return null;
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return nint.Zero;
        }
    }
}
