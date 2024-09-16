using Flashcards.Repository;
using Flashcards.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;
using System.Configuration;

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

        public int UpdateMenu()
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What do you want to update?")
                .PageSize(3)
                .AddChoices(new[]
                {
                    "1. Question", "2. Answer", "3. Both"
                })
                );

            int opt = int.Parse(choice.Substring(0, 1));
            return opt;

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

//TOMMOROW WORK (already added Stack Card Id)
// have to implement temporary flashcard id i.e for each stack id the card id will start from 1 and on dleetion of a flashcard the id will change accordingly i.e
// id 4 will change to 3 upon deletion of id 3
//(Done) - Review in morning
//Add edit and delete functionality


// Add a readme to provide sql script to create the database

/*## Setup Instructions

1.Set the following environment variables on your local machine:
   - `DB_SERVER`: Your SQL Server instance name
   - `DB_NAME`: Your database name

2. Modify the configuration file to use these environment variables:
   ```xml
<configuration>
       < appSettings >
           < add key = "FlashcardsDBConnection" value = "Data Source=${DB_SERVER};Initial Catalog=${DB_NAME};Integrated Security=True;" />
       </ appSettings >
   </ configuration >*/
