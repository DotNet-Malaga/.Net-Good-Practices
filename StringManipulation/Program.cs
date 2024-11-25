
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;


BenchmarkSwitcher.FromAssembly(typeof(StringConcat).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class StringConcat
{
    [Benchmark]
    public string Test()
    {
        string result = "";
        for (int i = 0; i < 10_000; i++)
        {
            result += i.ToString();
        }
        return result;
    }


    [Benchmark]
    public string TestOptimizado()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10_000; i++)
        {
            sb.Append(i.ToString());
        }

        return sb.ToString();
    }


    [Benchmark]
    public string ShortTest()
    {
        string result = "";
        for (int i = 0; i < 10; i++)
        {
            result += i.ToString();
        }
        return result;
    }


    [Benchmark]
    public string ShortTestOptimizado()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10; i++)
        {
            sb.Append(i.ToString());
        }

        return sb.ToString();
    }
}


[ShortRunJob]
[MemoryDiagnoser]
public class StringAllocationTests
{
   


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
