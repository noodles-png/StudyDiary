using StudyDiary.Core.Models;
using StudyDiary.Core.Services;

IEntryManager manager = new InMemoryEntryManager();

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

while (true)
{
    PrintMenu();
    string choice = Console.ReadLine() ?? "";


    switch (choice.Trim())
    {
        case "1": NewEntry(manager, study); break;
        case "2": PrintEntries(manager, study); break;
        case "3": DeleteEntry(manager); break;
        case "0": return;
        default: Console.WriteLine("Unbekannte Auswahl"); break;
    }
}

void PrintMenu()
{
    Console.WriteLine("=== Studien-Tagebuch ===");
    Console.WriteLine("1 - Neuer Eintrag");
    Console.WriteLine("2 - Alle Einträge anzeigen");
    Console.WriteLine("3 - Eintrag löschen");
    Console.WriteLine("0 - Beenden");
    Console.WriteLine("---");
    Console.WriteLine("Eingabe");
}

void NewEntry(IEntryManager manager, Study study)
{
    var entry = new Entry
    {
        StudyCode = study.Code,
        ParticipantCode = "Tester1",
        ProductId = 1,
        EntryTime = DateTimeOffset.Now
    };

    foreach (var question in study.Questions.OrderBy(q => q.Position))
    {
        string value = question.Type switch
        {
            QuestionType.YesNo => ReadYesNo(question.Text),
            QuestionType.Number => ReadNumber(question.Text),
            QuestionType.Text => ReadText(question.Text),
            _ => throw new NotSupportedException($"Fragetype {question.Type} noch nicht unterstützt")
        };

        entry.Answers.Add(new Answer { QuestionId = question.Id, Value = value });
    }

    manager.Save(entry);
    Console.WriteLine("Eintrag gespeichert.");
}

void DeleteEntry(IEntryManager manager)
{
    Console.WriteLine("Welche ID?");
    int idChoice = Convert.ToInt32(Console.ReadLine() ?? "");
}

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

string ReadYesNo(string prompt)
{
    while (true)
    {
        Console.Write($"{prompt} (j/n): ");
        string userInput = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

        if (userInput == "j")
        {
            return "true";
        }

        if (userInput == "n")
        {
            return "false";
        }

        Console.WriteLine("Bitte j oder n eingeben.");
    }
}

string ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write($"{prompt} (Zahl eingeben): ");
        string userInput = (Console.ReadLine() ?? "").Trim();
        if (int.TryParse(userInput, out int number))
        {
            return userInput;
        }
        
    }
}

string ReadText(string prompt)
{
    while (true)
    {
        Console.Write($"{prompt} : ");
        if ()
    }
}

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
manager.Save(entry1);
manager.Save(entry2);

// Test - Update
entry2.Answers[0].Value = "5";
manager.Save(entry2);
Console.WriteLine(manager.ReadAll().Count);
Console.WriteLine(manager.Delete(Guid.NewGuid()));

// Test - ReadAll
PrintEntries(manager, study);

// Test - Delete

if (manager.Delete(entry1.Id))
{
    Console.WriteLine("Gelöscht");
}

PrintEntries(manager, study);