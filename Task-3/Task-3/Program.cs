using System;
using System.Collections.Generic;

public class FunctionCache<TKey, TResult>
{
    private class CacheEntry
    {
        public TResult Value { get; }
        public DateTime ExpirationTime { get; }
        public CacheEntry(TResult value, DateTime expirationTime)
        {
            Value = value;
            ExpirationTime = expirationTime;
        }
    }

    private readonly Dictionary<TKey, CacheEntry> cache = new Dictionary<TKey, CacheEntry>();
    private readonly TimeSpan defaultExpiration;
    public FunctionCache(TimeSpan expiration)
    {
        defaultExpiration = expiration;
    }
    public delegate TResult Func(TKey key);
    public TResult GetOrAdd(TKey key, Func func)
    {
        if (cache.TryGetValue(key, out var entry))
        {
            if (DateTime.Now <= entry.ExpirationTime)
                return entry.Value;
            cache.Remove(key);
        }
        var result = func(key);
        cache[key] = new CacheEntry(result, DateTime.Now.Add(defaultExpiration));
        return result;
    }
    public void Clear() => cache.Clear();
}

class Program
{
    static void Main()
    {
        var cache = new FunctionCache<int, string>(TimeSpan.FromSeconds(5));
        string ExpensiveFunction(int key)
        {
            Console.WriteLine($"Обчислення для ключа: {key}");
            return $"Result for {key}";
        }
        Console.WriteLine(cache.GetOrAdd(1, ExpensiveFunction)); 
        Console.WriteLine(cache.GetOrAdd(1, ExpensiveFunction)); 
        System.Threading.Thread.Sleep(6000); 
        Console.WriteLine(cache.GetOrAdd(1, ExpensiveFunction)); 
    }
}