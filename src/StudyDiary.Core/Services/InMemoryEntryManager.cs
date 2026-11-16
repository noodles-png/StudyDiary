using System.Data;
using System.Linq;
using StudyDiary.Core.Models;

namespace StudyDiary.Core.Interfaces;

public class InMemoryEntryManager : IEntryManager
{
    private readonly List<Entry> _entries = [];

    public void Save(Entry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        int index = _entries.FindIndex(e => e.Id == entry.Id);

        if (index < 0)
        {
            _entries.Add(entry);
        }
        else
        {
            _entries[index] = entry;
        }
    }


    public IReadOnlyList<Entry> ReadAll()
    {
        return _entries.AsReadOnly();
    }

    public Entry? ReadById(Guid id)
    {
        return _entries.FirstOrDefault(e => e.Id == id);
    }


    public IReadOnlyList<Entry> ReadByProduct(int productId)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid id)
    {
        return _entries.RemoveAll(e => e.Id == id) > 0;
    }
}