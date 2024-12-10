using ConcurrentDataApp.Interfaces;
using ConcurrentDataApp.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        IGenerator _generator = new Generator(new Random());
        int numberOfTasks = 1000;
        var phoneBook = new ConcurrentDictionary<string, string>();
        var cancellationTokenSource = new System.Threading.CancellationTokenSource();

        var tasks = Enumerable.Range(0, numberOfTasks).Select(Print).ToList();

        var checkTask = Check();

        await Task.Delay(5000);
        cancellationTokenSource.Cancel();

        tasks.Add(checkTask);
        await Task.WhenAll(tasks);

        async Task Print(int i)
        {
            string phoneNumber = await _generator.GenerateNumberAsync();
            string name = await _generator.GenerateNameAsync();

            Console.WriteLine($"{name} - {phoneNumber}");

            phoneBook.TryAdd(phoneNumber, name);
            await Task.Delay(1000);
        }

        async Task Check()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                var results = phoneBook
                        .Where(entry => entry.Value.Count(c => c == 'b') >= 2)
                        .ToList();

                foreach (var entry in results)
                {
                    Console.WriteLine($"Match found! Phone: {entry.Key}, Name: {entry.Value}");
                }
                await Task.Delay(500);
            }
        }
    }
}

