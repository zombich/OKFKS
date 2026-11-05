using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.IO;
using System.IO.Pipes;

namespace LabWork7
{
    public class ScriptingHost
    {
        private ScriptOptions scriptOptions { get; set; }
        public ScriptingHost()
        {
            scriptOptions = ScriptOptions.Default.WithReferences(AppDomain.CurrentDomain.GetAssemblies()).WithImports("System", "System.Collections.Generic", "System.Linq", "LabWork7.MyActivity");
        }
        public async Task ExecuteAsync(string code)
        {
            await CSharpScript.EvaluateAsync(code, scriptOptions, new Automation());
        }

        public async Task Run()
        {
            while (true)
            {
                using (NamedPipeServerStream server = new NamedPipeServerStream("mypipe", PipeDirection.In))
                {
                    server.WaitForConnection();
                    await HandleClient(server);
                }
            }
        }

        public async Task HandleClient(NamedPipeServerStream server)
        {
            if (server.IsConnected)
            {
                using (StreamReader reader = new StreamReader(server))
                {
                    var message = await reader.ReadToEndAsync();
                    await ExecuteAsync(message);
                }
            }
        }

    }
}
