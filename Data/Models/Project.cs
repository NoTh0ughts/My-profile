namespace Data.Models;

public class Project
{
    public int ID { get; set; }

    public string RepositoryName { get; set; } = "";
    public string MainTitle { get; set; } = "";
    public string SubTitle { get; set; } = "";
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Technology> Technologies { get; set; } = new List<Technology>();
}