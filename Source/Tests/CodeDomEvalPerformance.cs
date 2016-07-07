using CSScriptLibrary;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

public class CodeDomEvalPerformance
{
    private readonly ITestOutputHelper _output;
    static string classCode = @"public class ScriptedClass
                                {
                                    public string HelloWorld {get;set;}
                                    public ScriptedClass()
                                    {
                                        HelloWorld = ""Hello CodeDom!"";
                                    }

                                    public string Test()
                                    {
                                        //System.Diagnostics.Debugger.Break();
                                        HelloWorld = ""Just testing..."";
                                        #if DEBUG
                                        return ""Debug testing"";
                                        #else
                                        return ""Release testing"";
                                        #endif
                                    }
                                }";

    public CodeDomEvalPerformance(ITestOutputHelper output)
    {
        _output = output;
        CSScript.CacheEnabled = false;
    }


    [Fact]
    public void CompileCodeGetsSlower()
    {
        for (int i = 0; i < 1000; i++)
        {
            var timer = Stopwatch.StartNew();
            CSScript.CodeDomEvaluator.CompileCode(classCode);
            timer.Stop();
            _output.WriteLine($"CompileCode took {timer.ElapsedMilliseconds} ms");
        }
       
    }    
}