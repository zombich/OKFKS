using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

var scriptOptions = ScriptOptions.Default.WithReferences(AppDomain.CurrentDomain.GetAssemblies()).WithImports("System", "System.Linq");

await CSharpScript.EvaluateAsync("Console.WriteLine(10);", scriptOptions);


class Automation
{
    void Print()
    {

    }

}
