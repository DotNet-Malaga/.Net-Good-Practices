
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Humanizer.Performance;


Tests t = new();
for (int i = 0; i < 10_000; i++)
{
    t.Test();
}


//BenchmarkSwitcher.FromAssembly(typeof(Tests).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class Tests
{
    [Benchmark]
    public string Test() =>
    Truncator.FixedLength.Truncate(
        "HEllo, build! How are you?", 8, "...");

}
