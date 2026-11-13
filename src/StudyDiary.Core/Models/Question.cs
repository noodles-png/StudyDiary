namespace StudyDiary.Core.Models;

public class Question
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public QuestionType Type { get; set; }
    public bool IsRequired { get; set; }
    public int Position { get; set; }
    
    // only for numbers, else null
    public int? Min { get; set; }
    public int? Max { get; set; }
    
    
    // only for choices
    public List<string> Options { get; set; } = [];
    
}