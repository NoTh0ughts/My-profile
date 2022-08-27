namespace Data.Models;

public class Technology
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<Project> Projects { get; set; }
}