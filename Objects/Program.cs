
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


BenchmarkSwitcher.FromAssembly(typeof(Lookups).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class Lookups
{
    public List<Person> Persons { get; set; }
    public Dictionary<string, Person> PersonsDict { get; set; }
    public HashSet<string> PersonsHashSet { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Persons = new List<Person>();
        PersonsDict = new Dictionary<string, Person>();
        PersonsHashSet = new HashSet<string>();
        for (int i = 0; i < 10_000; i++)
        {
            var person = new Person
            {
                Name = "Name" + i,
                Surname = "Surname" + i
            };
            Persons.Add(person);
            PersonsDict.Add(person.Name, person);
            PersonsHashSet.Add(person.Name);
        }
    }


    [Benchmark]
    public int ObjectFinderList()
    {
        int found = 0;
        var rnd = new Random();
        for (int i = 0; i < 10_000; i++)
        {
            found += Persons.FirstOrDefault(p => p.Name == $"Name{rnd.Next(1, 100_000)}") != null ? 1 : 0;
        }
        return found;
    }

    [Benchmark]
    public int ObjectFinderDict()
    {
        int found = 0;
        var rnd = new Random();
        for (int i = 0; i < 10_000; i++)
        {
            found += PersonsDict.ContainsKey($"Name{rnd.Next(1, 100_000)}") ? 1 : 0;
        }
        return found;
    }

    [Benchmark]
    public int ObjectFinderHashSet()
    {
        int found = 0;
        var rnd = new Random();
        for (int i = 0; i < 10_000; i++)
        {
            found += PersonsHashSet.Contains($"Name{rnd.Next(1, 100_000)}") ? 1 : 0;
        }
        return found;
    }
}


public class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
