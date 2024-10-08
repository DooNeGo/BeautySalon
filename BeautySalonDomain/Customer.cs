﻿namespace BeautySalon.Domain;

public sealed record Customer
{
    private Customer() { }

    public Customer(string lastName, string firstName, string? middleName, string phone)
    {
        Id = Guid.NewGuid();
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Phone = phone;
    }

    public Guid Id { get; }

    public string LastName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string Phone { get; set; } = string.Empty;
    
    public Guid UserId { get; init; }

    public List<Appointment> Appointments { get; set; } = [];
}