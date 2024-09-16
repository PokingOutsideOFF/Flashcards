using Flashcards.Repository;
using Flashcards.Views;
using Spectre.Console;
using System.Diagnostics;

namespace Flashcards.Services
{
    public class StudySessionService
    {
        private DatabaseContext _context;
        public StudySessionService(DatabaseContext context)
        {
            _context = context;
        }

        public void SelectOperation(int choice)
        {
            var studyRepo = new StudySessionRepository(_context);
            var flashcardView = new FlashcardView(_context);

            UserInput userInput = new UserInput();
            if (choice == 3)
            {
                string stackName = flashcardView.GetStackName();
                var stack = new StackRepository(_context);
                int stackId = stack.GetStackId(stackName);
                var cardRepo = new FlashcardRepository(_context, stackId);

                var cards = cardRepo.GetAllCards();
                Console.Clear();
                Console.WriteLine("Starting Study Session: \n\n");
                DateTime startDate = DateTime.Now;
                Stopwatch timer = new Stopwatch();

                //Extract question and answer from database;
                int score = 0;
                string duration;
                timer.Start();
                foreach (var card in cards)
                {
                    Console.Write(card.Question + ": " );
                    string ans = userInput.GetText();
                    if(ans.ToLower().Trim() == card.Answer.ToLower().Trim())
                    {
                        AnsiConsole.Markup("[blue]Correct answer[/]\n\n");
                        score++;
                    }
                    else
                        AnsiConsole.Markup("[red]Wrong answer[/]\n\n");
                    Console.Write("Press 1 to continue: ");
                    int num = userInput.GetInt();
                    if (num != 1)
                    {
                        timer.Stop();
                        duration = timer.Elapsed.ToString();
                        studyRepo.Insert(stackId, stackName, score, startDate, duration);
                        AnsiConsole.Markup("\n[red]Returning to Main Menu...[/]");
                        Thread.Sleep(1000); 
                        Console.Clear();
                        return;
                    }
                    Console.WriteLine();
                }
                timer.Stop();
                Console.WriteLine("Yayy! You completed the Stack");

                duration = timer.Elapsed.ToString();
                studyRepo.Insert(stackId, stackName, score, startDate, duration);
            }

            else if(choice == 4)
            {
                studyRepo.GetSession();
            }
            AnsiConsole.Markup("\n[blue]Press enter to continue....[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
