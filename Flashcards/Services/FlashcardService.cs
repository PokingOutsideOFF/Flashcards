using Flashcards.Repository;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Services
{
    public class FlashcardService
    {
        private DatabaseContext context;

        public FlashcardService(DatabaseContext context)
        {
            this.context = context;
        }

        public void SelectOperation(int opt, string stackName, int stackId)
        {
            var repo = new FlashcardRepository(context, stackId);
            UserInput userInput = new UserInput();
            switch (opt)
            {
                case 3:
                    repo.GetAllCards();
                    break;
                case 4:
                    Console.Write("How many flashcards you want to view?: ");
                    int limit = userInput.GetInt();
                    repo.GetXCards(limit);
                    break;
                case 5:
                    string response;
                    do
                    {
                        Console.Write("Enter Question: ");
                        string question = userInput.GetText();
                        Console.Write("Enter Answer: ");
                        string answer = userInput.GetText();

                        repo.Insert(question, answer);

                        Console.Write("\nPress y to add another flashcard: ");
                        response = userInput.GetText();
                        Console.WriteLine();

                    } while (response.ToLower() == "y");
                    break;

            }
            AnsiConsole.Markup("[blue]Press enter to continue....[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
