namespace StudyDiary.Core.Models;

public class Study
{
    public required string Code { get; set; } = "";
    public string Name { get; set; } = "";
    
    public DateOnly BeginDate { get; set; }
    public DateOnly EndDate { get; set; }

    public List<Product> Products { get; set; } = [];
    public List<Question> Questions { get; set; } = [];
}