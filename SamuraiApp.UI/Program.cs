using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            AddSamuraisByName("Julie", "Sampson", "Skeet", "Cube");
            GetSamurais();
            AddVariousTypes();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static async void AddVariousTypes()
        {
           await _context.Samurais.AddRangeAsync(
                new Samurai { Name = "Shimada" },
                new Samurai { Name = "Okamoto"},
                new Samurai { Name = "Anegawa"},
                new Samurai { Name = "Nagashino"});
           await _context.SaveChangesAsync();
        }

        private static async void AddSamuraisByName(params string[] names)
        {
            foreach (string name in names)
            {
                await _context.Samurais.AddAsync(new Samurai { Name = name });
            }
           await _context.SaveChangesAsync();
        }

        private static async void GetSamurais()
        {
            var samurais = await _context.Samurais
                .TagWith("ConsoleApp.Program.GetSamurais method")
                .ToListAsync();
            Console.WriteLine($"Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void QueryFilters()
        {
            var samurais = _context.Samurais.Where(s => s.Name == "Sampson").ToList();
        }
    }
}
