using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T>
{
    private readonly List<T> items = new List<T>();

    public void Add(T item) => items.Add(item);

    public bool Remove(T item) => items.Remove(item);

    public IEnumerable<T> GetAll() => items;

    public delegate bool Criteria(T item);

    public IEnumerable<T> Find(Criteria criteria) => items.Where(item => criteria(item));
}

class Program
{
    static void Main()
    {
        var intRepository = new Repository<int>();
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);
        intRepository.Add(4);

        Console.WriteLine("Всі елементи в репозиторії чисел:");
        foreach (var item in intRepository.GetAll())
            Console.WriteLine(item);
        var greaterThanTwo = new Repository<int>.Criteria(x => x > 2);
        Console.WriteLine("\nЧисла більше 2:");
        foreach (var item in intRepository.Find(greaterThanTwo))
            Console.WriteLine(item);
        var stringRepository = new Repository<string>();
        stringRepository.Add("apple");
        stringRepository.Add("banana");
        stringRepository.Add("cherry");
        Console.WriteLine("\nВсі елементи в репозиторії рядків:");
        foreach (var item in stringRepository.GetAll())
            Console.WriteLine(item);
        var startsWithB = new Repository<string>.Criteria(s => s.StartsWith("b"));
        Console.WriteLine("\nРядки, що починаються на 'b':");
        foreach (var item in stringRepository.Find(startsWithB))
            Console.WriteLine(item);
    }
}
