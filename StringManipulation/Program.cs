
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;


BenchmarkSwitcher.FromAssembly(typeof(StringConcat).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class StringConcat
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


[ShortRunJob]
[MemoryDiagnoser]
public class StringAllocationTests
{
    //[Benchmark]
    //public void BoxingTest()
    //{
    //    Random rand = new Random();
    //    for (int i = 0; i < 90_000; i++)
    //    {
    //        object boxed = rand.Next(i, 100_000_000) * i;
    //    }
    //}



    //[Benchmark]
    //public void BoxingTestOK()
    //{
    //    Random rand = new Random();
    //    for (int i = 0; i < 90_000; i++)
    //    {
    //        int unboxed = rand.Next(i, 100_000_000) * i;
    //    }
    //}



    [Benchmark]
    public void ToUpperKO()
    {
        List<Person> persons = new List<Person>();
        for (int i = 0; i < 100_000; i++)
        {
            string name = $"Name{i}";
            string surname = $"Surname{i}";
            persons.Add(new Person
            {
                Name = name.ToUpper(),
                Surname = surname.ToUpper(),
                FullName = $"{name} {surname}".ToUpper(),
            });
        }
    }

    [Benchmark]
    public void ToUpperOK()
    {
        List<Person> persons = new List<Person>();
        string name = "Name".ToUpper();
        string surname = "Surname".ToUpper();
        for (int i = 0; i < 100_000; i++)
        {
            persons.Add(new Person
            {
                Name = $"{name}{i}",
                Surname = $"{surname}{i}",
                FullName = $"{name}{i} {surname}{i}",
            });
        }
    }


    [Benchmark]
    public void ToUpperOK2()
    {
        List<Person2> persons = new List<Person2>();
        string name = "Name".ToUpper();
        string surname = "Surname".ToUpper();
        for (int i = 0; i < 100_000; i++)
        {
            persons.Add(new Person2
            {
                Name = $"{name}{i}",
                Surname = $"{surname}{i}"
            });
        }
    }
}

public class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName { get; set; }
}

public class Person2
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName
    {
        get
        {
            return $"{Name} {Surname}";
        }
    }
}
