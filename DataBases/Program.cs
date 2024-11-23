

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DataBases;
using DataBases.Context;
using Microsoft.EntityFrameworkCore;



[MemoryDiagnoser]
public class DatabaseBenchmark
{

    [Benchmark]
    public decimal LoadCustomersCalculateAverageSalary()
    {
        using var ctx = new AppDbContext();

        decimal sum = 0;
        var count = 0;
        foreach (var blog in ctx.Customer)
        {
            sum += blog.Salary;
            count++;
        }

        return sum / count;
    }
    [Benchmark]
    public decimal LoadCustomersNoTrackingCalculateAverageSalary()
    {
        using var ctx = new AppDbContext();

        decimal sum = 0;
        var count = 0;
        foreach (var blog in ctx.Customer.AsNoTracking())
        {
            sum += blog.Salary;
            count++;
        }

        return sum / count;
    }
    [Benchmark]
    public decimal LoadCustomersProjectOnlyCalculateAverageSalary()
    {
        using var ctx = new AppDbContext();

        decimal sum = 0;
        var count = 0;
        foreach (var salary in ctx.Customer.Select(c => c.Salary))
        {
            sum += salary;
            count++;
        }

        return sum / count;
    }
    [Benchmark(Baseline = true)]
    public decimal CalculateInDatabase()
    {
        using var ctx = new AppDbContext();
        return ctx.Customer.Average(b => b.Salary);
    }

    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<DatabaseBenchmark>(); 
    }
}

