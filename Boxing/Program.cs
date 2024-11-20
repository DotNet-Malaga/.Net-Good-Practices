
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


//BoxingTests t = new();
//for (int i = 0; i < 10_000; i++)
//{
//    t.BoxingTest();
//}


BenchmarkSwitcher.FromAssembly(typeof(BoxingTests).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class BoxingTests
{
    [Benchmark]
    public void BoxingTest()
    {
        for (int i = 0; i < 90_000; i++)
        {
            object boxed = 10;
        }
    }



    [Benchmark]
    public void BoxingTestOK()
    {
        for (int i = 0; i < 90_000; i++)
        {
            int unboxed = 10;
        }
    }

}
