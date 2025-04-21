using lab2;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class InMemoryCrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
{
    private readonly ConcurrentDictionary<Guid, T> _data = new ConcurrentDictionary<Guid, T>();
    private readonly string _filePath;

    public InMemoryCrudServiceAsync(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<bool> CreateAsync(T element)
    {
        var id = GetId(element);
        return _data.TryAdd(id, element);
    }

    public Task<T> ReadAsync(Guid id)
    {
        _data.TryGetValue(id, out var element);
        return Task.FromResult(element);
    }

    public Task<IEnumerable<T>> ReadAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_data.Values.ToList());
    }

    public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        var result = _data.Values
            .Skip((page - 1) * amount)
            .Take(amount)
            .ToList();
        return Task.FromResult<IEnumerable<T>>(result);
    }

    public Task<bool> UpdateAsync(T element)
    {
        var id = GetId(element);
        _data[id] = element;
        return Task.FromResult(true);
    }

    public Task<bool> RemoveAsync(T element)
    {
        var id = GetId(element);
        return Task.FromResult(_data.TryRemove(id, out _));
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_data.Values);
            File.WriteAllText(_filePath, json); // Замість WriteAllTextAsync використовується WriteAllText
            return true;
        }
        catch
        {
            return false;
        }
    }

    private Guid GetId(T element)
    {
        var prop = typeof(T).GetProperty("Id");
        if (prop == null)
            throw new InvalidOperationException("Тип не має властивості 'Id'");
        return (Guid)prop.GetValue(element);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _data.Values.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
