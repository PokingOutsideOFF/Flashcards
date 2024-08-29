using Flashcards.Models;
using Spectre.Console;
using System;

namespace Flashcards.Repository
{
    public class StackRepository
    {
        private readonly DatabaseContext _context;

        public StackRepository(DatabaseContext context)
        {
            _context = context;
        }

        /*  (int?) s.StackNumber: Casting StackNumber to a nullable integer (int?) ensures that Max returns null if there are no records.
        *  This prevents errors when trying to compute the maximum value on an empty table.
        ?? 0: The null-coalescing operator ?? ensures that maxNumber is set to 0 if Max returns null. This allows the first StackNumber to start at 1.*/
       public void Insert(string Stack_Name)
        {
            var maxNumber = _context.Stack.Max(s => (int?)s.StackNumber) ?? 0;
            var stack = new Stack
            {
                StackNumber = maxNumber + 1,
                StackName = Stack_Name
            };
            _context.Add(stack);
            AnsiConsole.Markup("[red]Row inserted[/]\n");
            _context.SaveChanges();
        }

        public int GetStack()
        {
            var allStacks = _context.Stack.ToList();
            if(allStacks.Count == 0)
            {
                AnsiConsole.Markup("[red]Table is empty...[/]\n");
                Thread.Sleep(1000);
                return 0;
            }

            AnsiConsole.Markup("\n[blue]Stack Table[/]\n");
            var table = new Table();
            table.AddColumn("Stack Id");
            table.AddColumn("Stack Name");
            foreach (var stack in allStacks)
            {
                table.AddRow(
                    stack.StackNumber.ToString(),
                    stack.StackName);
            }
            AnsiConsole.Write(table);
            return allStacks.Count;
        }

        public void Update(int id, string Stack_Name)
        {
            var stack = new Stack
            {
                StackId = id,
                StackName = Stack_Name
            };
            _context.Update(stack);
            AnsiConsole.Markup("[red]Row updated[/]");
            _context.SaveChanges();
        }

        public void Delete(int number)
        {
            var query = _context.Stack.FirstOrDefault(s => s.StackNumber == number);
            if(query != null)
            {
                _context.Remove(query);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Stack not found");
                return;
            }

            
            var entities = _context.Stack.Where(s => s.StackNumber > number).ToList();
            foreach(var entity in entities)
            {
                entity.StackNumber -= 1;
            }
            AnsiConsole.Markup("[red]Row deleted[/]\n");
            _context.SaveChanges();
        }
    }
}
