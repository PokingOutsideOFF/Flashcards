using Flashcards.Models;
using Spectre.Console;
using System;
using System.Security.Cryptography;

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
           // var maxNumber = _context.Stack.Max(s => (int?)s.StackId) ?? 0
            var stack = new Stack
            {
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
            table.AddColumn("Stack Name");

            foreach (var stack in allStacks)
            {
                table.AddRow(
                    stack.StackName);
            }
            AnsiConsole.Write(table);
            return allStacks.Count;
        }

        public void Update(string Stack_Name)
        {
            var stack = new Stack
            {
                StackName = Stack_Name
            };
            _context.Update(stack);
            AnsiConsole.Markup("[red]Row updated[/]\n\n");
            _context.SaveChanges();
        }

        public void Delete(string Stack_Name)
        {
            var query = _context.Stack.FirstOrDefault(s => s.StackName.ToLower() == Stack_Name.ToLower());

            if(query != null)
            {
                _context.Remove(query);
                _context.SaveChanges();
                GetStack();
            }
            else
            {
                Console.WriteLine("Stack not found\n\n");
                return;
            }

            AnsiConsole.Markup("[red]Row deleted[/]\n\n");
            _context.SaveChanges();
        }

        public bool CheckNameExists(string stackName)
        {
            var query = _context.Stack.Where(s => s.StackName == stackName).ToList();
            if(query.Count == 0) return false;
            return true;
        }

        public int GetStackId(string stackName)
        {
            int stackId = _context.Stack
                          .Where(s => s.StackName == stackName)
                          .Select(s => s.StackId)
                          .FirstOrDefault();
            return stackId;
        }

    }
}
