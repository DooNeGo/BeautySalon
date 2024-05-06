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

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<Service> Services { get; set; } = [];
}