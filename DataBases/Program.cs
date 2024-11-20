

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DataBases;
using DataBases.Context;
using Microsoft.EntityFrameworkCore;


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
    public async Task MeasureCustomerQueryNOPerformance1()
    {
        var customers = await _context.Customer.AsNoTracking()
            .Take(100).ToListAsync();
        var orders = new List<Order>();

        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                orders.AddRange(await _context.Order.Where(c => c.CustomerId == customer.Id).ToListAsync());
            }
        }
    }

    [Benchmark]
    public async Task MeasureCustomerQueryNOPerformance2()
    {
        var customers = (await _context.Customer
                                          .AsNoTracking()
                                          .Include(c => c.Orders)
                                          .ToListAsync());
    }

    [Benchmark]
    public async Task MeasureCustomerQueryNOPerformance3()
    {
        //for (var i = 0; i < 10_000; i++)
        //{

            var customers = (await _context.Customer
                                              .AsNoTracking()
                                              .Include(c => c.Orders)
                                              .ToListAsync()).OrderBy(c => c.Salary);
        //}
    }


    [Benchmark]
    public async Task MeasureCustomerQueryNOPerformance4()
    {
        var customers = (await _context.Customer
                                           .AsNoTracking()
                                          .Include(c => c.Orders)
                                          .OrderBy(c => c.Salary)
                                          .ToListAsync());
    }

    [Benchmark]
    public async Task MeasureCustomerQueryPerformance()
    {
        var customers = (await _context.Customer
                                       .AsNoTracking()
                                       .Include(c => c.Orders)
                                       .ToListAsync());
    }

    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<DatabaseBenchmark>(); 
    }
}

