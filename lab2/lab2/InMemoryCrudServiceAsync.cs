using Library.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class InMemoryCrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
{
    private readonly ConcurrentDictionary<Guid, T> _data = new();
    private readonly string _filePath;
    private readonly Func<T, Guid> _getId;

    public InMemoryCrudServiceAsync(string filePath)
    {
        _filePath = filePath;
        _getId = typeof(T).GetProperty("Id")?.GetValue as Func<T, Guid>;
    }

    public async Task<bool> CreateAsync(T element)
    {
        var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
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
        return Task.FromResult<IEnumerable<T>>(_data.Values.Skip((page - 1) * amount).Take(amount).ToList());
    }

    public Task<bool> UpdateAsync(T element)
    {
        var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
        _data[id] = element;
        return Task.FromResult(true);
    }

    public Task<bool> RemoveAsync(T element)
    {
        var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
        return Task.FromResult(_data.TryRemove(id, out _));
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_data.Values);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerator<T> GetEnumerator() => _data.Values.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
