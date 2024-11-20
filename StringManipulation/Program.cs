
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;


BenchmarkSwitcher.FromAssembly(typeof(Tests).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class Tests
{
    [Benchmark]
    public void Test()
    {
        string result = "";
        for (int i = 0; i < 10_000; i++)
        {
            result += i.ToString();
        }
    }


    [Benchmark]
    public void TestOptimizado()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10_000; i++)
        {
            sb.Append(i.ToString());
        }
    }
}
