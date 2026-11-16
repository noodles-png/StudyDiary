using StudyDiary.Core.Models;
using StudyDiary.Core.Services;

IEntryManager testManager = new InMemoryEntryManager();


var study = new Study
{
    Code = "Windel2026",
    Name = "",
    BeginDate = new DateOnly(2022, 1, 1),
    EndDate = new DateOnly(2022, 12, 31),

    Products =
    [
        new Product { Id = 1, Name = "ProduktA" },
        new Product { Id = 2, Name = "ProduktB" }
    ],

    Questions =
    [
        new Question
        {
            Id = 1, Text = "WindelNr", Type = QuestionType.Number, IsRequired = true, Position = 1, Min = 1, Max = 99
        },
        new Question { Id = 2, Text = "Ausgelaufen", Type = QuestionType.YesNo, IsRequired = true, Position = 2 },
        new Question { Id = 3, Text = "Stuhl", Type = QuestionType.YesNo, IsRequired = true, Position = 3 },
        new Question { Id = 4, Text = "Haut feucht", Type = QuestionType.YesNo, IsRequired = true, Position = 4 },
        new Question { Id = 5, Text = "Notizen", Type = QuestionType.Text, IsRequired = false, Position = 5 }
    ],
};


var entry1 = new Entry
{
    Answers =
    [
        new Answer { QuestionId = 1, Value = "3" },
        new Answer { QuestionId = 2, Value = "false" },
        new Answer { QuestionId = 3, Value = "true" },
        new Answer { QuestionId = 4, Value = "true" },
        new Answer { QuestionId = 5, Value = "TestNotes" }
    ],

    EntryTime = DateTimeOffset.Now,
    ParticipantCode = "Tester1",
    ProductId = 1,
    StudyCode = study.Code
};

var entry2 = new Entry
{
    Answers =
    [
        new Answer { QuestionId = 1, Value = "4" },
        new Answer { QuestionId = 2, Value = "true" },
        new Answer { QuestionId = 3, Value = "false" },
        new Answer { QuestionId = 4, Value = "false" },
        new Answer { QuestionId = 5, Value = "TestManager" }
    ],

    EntryTime = DateTimeOffset.Now,
    ParticipantCode = "Tester1",
    ProductId = 1,
    StudyCode = study.Code
};



// Test - Save
testManager.Save(entry1);
testManager.Save(entry2);

// Test - Update
entry2.Answers[0].Value = "5";
testManager.Save(entry2);
Console.WriteLine(testManager.ReadAll().Count);
Console.WriteLine(testManager.Delete(Guid.NewGuid()));

// Test - ReadAll
PrintEntries(testManager, study);

// Test - Delete

if (testManager.Delete(entry1.Id))
{
    Console.WriteLine("Gelöscht");
}

PrintEntries(testManager, study);



void PrintEntries(IEntryManager manager, Study study)
{
    foreach (var entry in manager.ReadAll())
    {
        Console.WriteLine($"Eintrag {entry.Id} vom {entry.EntryTime:dd.MM.yyyy HH:mm}");

        foreach (var answer in entry.Answers)
        {
            var question = study.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            string text = question?.Text ?? "(unbekannte Frage)";
            Console.WriteLine($"{text}: {answer.Value}");
        }
    }
}