

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using SyncTests.Context;
using SyncTests.Models;
using System.Runtime.CompilerServices;


BenchmarkSwitcher.FromAssembly(typeof(DatabaseBenchmark).Assembly).Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class DatabaseBenchmark
{
    private AppDbContext _context;

    public DatabaseBenchmark()
    {
        _context = new AppDbContext();
    }

    [Benchmark]
    public void MeasureCustomerQuerySync()
    {
        var customers = _context.Customer
             .Include(c => c.Orders).OrderBy(c => c.Salary).ToList();
    }

    [Benchmark]
    public async Task MeasureCustomerQueryAsync()
    {
        var customers = await _context.Customer
             .Include(c => c.Orders).OrderBy(c => c.Salary).ToListAsync();
    }

}