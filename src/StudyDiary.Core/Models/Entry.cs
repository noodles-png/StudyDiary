namespace StudyDiary.Core.Models;

public class Entry
{
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public required string StudyCode { get; set; }
    public required string ParticipantCode { get; set; }
    
    public int ProductId { get; set; }
    public required DateTimeOffset EntryTime { get; set; } 

    public List<Answer> Answers { get; set; } = [];
}