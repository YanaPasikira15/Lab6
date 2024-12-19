using System;
using System.Collections.Generic;

public class TaskScheduler<TTask, TPriority> where TPriority : IComparable<TPriority>
{
    public delegate void TaskExecution(TTask task);
    private readonly SortedDictionary<TPriority, Queue<TTask>> taskQueue = new();
    public void AddTask(TTask task, TPriority priority)
    {
        if (!taskQueue.ContainsKey(priority))
            taskQueue[priority] = new Queue<TTask>();

        taskQueue[priority].Enqueue(task);
    }
    public void ExecuteNext(TaskExecution execute)
    {
        if (taskQueue.Count == 0)
        {
            Console.WriteLine("Немає завдань у черзі.");
            return;
        }
        var highestPriority = GetHighestPriority();
        var task = taskQueue[highestPriority].Dequeue();

        if (taskQueue[highestPriority].Count == 0)
            taskQueue.Remove(highestPriority);
        execute(task);
    }
    private TPriority GetHighestPriority()
    {
        foreach (var priority in taskQueue.Keys)
            return priority;
        throw new InvalidOperationException("Черга пуста.");
    }
    public void DisplayTasks()
    {
        if (taskQueue.Count == 0)
        {
            Console.WriteLine("Черга порожня.");
            return;
        }

        Console.WriteLine("Завдання в черзі:");
        foreach (var kvp in taskQueue)
        {
            Console.WriteLine($"Пріоритет {kvp.Key}: {string.Join(", ", kvp.Value)}");
        }
    }
}

class Program
{
    static void Main()
    {
        var scheduler = new TaskScheduler<string, int>();
        Console.WriteLine("Введіть завдання (формат: <завдання>,<пріоритет>). Для завершення введіть 'exit'.");
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "exit") break;

            var parts = input.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1], out int priority))
            {
                scheduler.AddTask(parts[0], priority);
                Console.WriteLine($"Додано завдання: {parts[0]} з пріоритетом {priority}");
            }
            else
            {
                Console.WriteLine("Невірний формат. Спробуйте ще раз.");
            }
        }

        Console.WriteLine("\nВсі завдання у черзі:");
        scheduler.DisplayTasks();
        Console.WriteLine("\nВиконання завдань:");
        while (true)
        {
            Console.WriteLine("Виконати наступне завдання? (yes/no)");
            var command = Console.ReadLine();
            if (command == "no") break;

            scheduler.ExecuteNext(task => Console.WriteLine($"Виконано завдання: {task}"));
        }

        Console.WriteLine("Програма завершена.");
    }
}