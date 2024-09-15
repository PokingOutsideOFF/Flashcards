using Flashcards.Repository;
using Spectre.Console;

namespace Flashcards.Services
{
    public class StackService
    {
        private DatabaseContext _context;
        public StackService(DatabaseContext context)
        {
            _context = context;
        }

        public void SelectOperation(int choice)
        {
            var repo = new StackRepository(_context);
            var input = new UserInput();
            switch (choice)
            {
                case 2:
                    Console.Write("Enter Stack Name: ");
                    var stackName = input.GetText();

                    repo.Insert(stackName);
                    break;

                case 3:
                    repo.GetStack();
                    break;

                case 4:
                    int rows = repo.GetStack();
                    if (rows == 0) break;

                    /*Console.Write("Enter Stack Id to be Updated: ");
                    var stackId = input.GetInt();*/
                    AnsiConsole.Markup("\nEnter Stack Name to be [red]Updated[/]: ");
                    stackName = input.GetText();

                    repo.Update(stackName);
                    repo.GetStack();
                    break;

                case 5:
                    rows = repo.GetStack();
                    if (rows == 0) break;

                    AnsiConsole.Markup("Enter Stack Name to be [red]Deleted[/]: ");
                    stackName = input.GetText();

                    repo.Delete(stackName);

                    break;
            }
            AnsiConsole.Markup("[blue]Press enter to continue....[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
