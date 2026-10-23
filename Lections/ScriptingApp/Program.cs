using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

var scriptOptions = ScriptOptions.Default.WithReferences(typeof(Automation).Assembly).WithImports("System");

var automation = new Automation((text) =>
{
    Console.WriteLine(text);
});

var script = File.ReadAllText("script.csx");

//await CSharpScript.EvaluateAsync(script, scriptOptions, globals: automation, globalsType:typeof(Automation));

int result = await CSharpScript.EvaluateAsync<int>("1+2");
Console.WriteLine(result);

Console.WriteLine(automation.PrintCount);

public class Automation
{
    private Action<string> _showMessage;
    public int PrintCount { get; set; } = 0;

    public Automation(Action<string> showMessage)
    {
        _showMessage = showMessage;
    }

    public void Print(string text)
    {
        _showMessage(text);
        PrintCount++;
    }

}
