using StudyDiary.Core.Models;

namespace StudyDiary.Core.Interfaces;

public interface IEntryManager
{
    void Save(Entry entry);

    IReadOnlyList<Entry> ReadAll();

    Entry? ReadById(Guid id);

    IReadOnlyList<Entry> ReadByProduct(int productId);

    bool Delete(Guid id);
}