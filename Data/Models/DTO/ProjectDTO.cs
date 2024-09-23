namespace Data.Models.DTO;

public class ProjectDTO
{
    public int ID { get; set; }

    public string RepositoryName { get; set; } = "";
    public string MainTitle { get; set; } = "";
    public string SubTitle { get; set; } = "";
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public IEnumerable<TechnologyDTO> Technologies { get; set; } = new TechnologyDTO[]{};
}

public class TechnologyDTO
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
}