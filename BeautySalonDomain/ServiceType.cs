namespace BeautySalon.Domain;

public sealed record ServiceType
{
    private ServiceType() { }

    public ServiceType(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
    }

    public Guid Id { get; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<Service> Services { get; set; } = null!;
}