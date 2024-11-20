
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Objects.Models;


BenchmarkSwitcher.FromAssembly(typeof(TestObjects).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class TestObjects
{
    [Benchmark]
    public void ObjectTest()
    {
        var faker = new Faker();

        for (int i = 0; i < 40_000; i++)
        {
            var obj = new User
            {
                Name = faker.Name.FullName(),
                Teams = faker.PickRandom("Technology", "Business", "Science", "Education", "Art"),
                Position = faker.PickRandom("Manager", "Developer", "Designer", "Analyst", "Administrator")
            };
        }
    }


    [Benchmark]
    public void ObjectTestOptimizado()
    {
        var faker = new Faker();

        var obj = new User();
        for (int i = 0; i < 40_000; i++)
        {
            obj.Name = faker.Name.FullName();
            obj.Teams = faker.PickRandom("Technology", "Business", "Science", "Education", "Art");
            obj.Position = faker.PickRandom("Manager", "Developer", "Designer", "Analyst", "Administrator");
        }
    }
}
