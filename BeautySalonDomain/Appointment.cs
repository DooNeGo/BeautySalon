﻿namespace BeautySalon.Domain;

public sealed record Appointment
{
    private Appointment() { }

    public Appointment(DateOnly date, TimeOnly time, Master master, List<Service> services, Customer customer)
    {
        Id = Guid.NewGuid();
        Date = date;
        Time = time;
        Master = master;
        Services = services;
        Customer = customer;
    }

    public Guid Id { get; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public Master Master { get; set; } = null!;

    public List<Service> Services { get; set; } = null!;

    public Customer Customer { get; set; } = null!;
}
