﻿namespace DataBases;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Salary { get; set; }
    public ICollection<Order> Orders { get; set; }
}

