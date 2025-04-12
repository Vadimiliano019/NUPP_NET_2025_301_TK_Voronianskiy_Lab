using System.Text.Json;

namespace Library.Common;

public class CrudService<T> : ICrudService<T> where T : class
{
    private readonly Dictionary<Guid, T> _items = new();

    public void Create(T element)
    {
        var id = (Guid)element!.GetType().GetProperty("Id")!.GetValue(element)!;
        _items[id] = element;
    }

    public T Read(Guid id)
    {
        return _items.TryGetValue(id, out var element) ? element : null!;
    }

    public IEnumerable<T> ReadAll()
    {
        return _items.Values;
    }

    public void Update(T element)
    {
        var id = (Guid)element!.GetType().GetProperty("Id")!.GetValue(element)!;
        if (_items.ContainsKey(id))
        {
            _items[id] = element;
        }
    }

    public void Remove(T element)
    {
        var id = (Guid)element!.GetType().GetProperty("Id")!.GetValue(element)!;
        _items.Remove(id);
    }

    public void Save(string filePath)
    {
        var json = JsonSerializer.Serialize(_items);
        File.WriteAllText(filePath, json);
    }

    public void Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var restored = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json);
            if (restored != null)
            {
                foreach (var kv in restored)
                {
                    _items[kv.Key] = kv.Value;
                }
            }
        }
    }
}
