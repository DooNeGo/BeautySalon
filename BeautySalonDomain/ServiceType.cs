namespace BeautySalon.Domain;

public sealed record ServiceType
{
    private ServiceType() { }

    public ServiceType(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<Service> Services { get; set; } = null!;
}