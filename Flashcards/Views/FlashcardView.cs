﻿using Flashcards.Repository;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.Views
{
    public class FlashcardView
    {
        private DatabaseContext _context;

        public FlashcardView(DatabaseContext context)
        {
            _context = context;
        }

        public string GetStackName()
        {
            while (true)
            {
                Console.Write("Enter Stack Name: ");
                string stackName = Console.ReadLine();
                var stackView = new StackRepository(_context);
                if (!stackView.CheckNameExists(stackName.ToLower()))
                {
                    AnsiConsole.Markup("[red]Stack doesn't exist. Enter again.[/]\n");
                }
                else return stackName;
            }
        }

        public void Menu()
        {
            string stackName = GetStackName();
           
            while (true)
            {
                
                AnsiConsole.Markup($"\nCurrent stack: [blue] {stackName} [/]\n\n");
                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Manage Flashcard Menu")
                    .PageSize(7)
                    .AddChoices(new[]
                    {
                    "1. Return to Main Menu", "2. Change Current Stack", "3. View all Flashcards", "4. View X amount of Flashcards",
                    "5. Create Flashcard", "6. Edit Flashcard", "7. Delete Flashcard"
                    })
                    );

                var service = new FlashcardService(_context);
                int opt = int.Parse(choice.Substring(0, 1));
                if (opt == 1) break ;

                if(opt == 2)
                {
                    Console.Clear();
                    stackName = GetStackName();
                    continue;
                }
                var stack = new StackRepository(_context);
                int stackId = stack.GetStackId(stackName);
                service.SelectOperation(opt, stackName, stackId);
            }
            Console.Clear();
        }
    }
}
