
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Humanizer.Performance;


//Tests t = new();
//for (int i = 0; i < 10_000; i++)
//{
//    t.Test2();
//}


BenchmarkSwitcher.FromAssembly(typeof(Tests).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class Tests
{
    [Benchmark]
    public string Test() =>
    Truncator.FixedLength.Truncate(
        "Hllo, build! How are you?", 8, "...");


    [Benchmark]
    public string Test2() =>
        To.Transform("Hello, World! How are you?", To.SentenceCase);

}
