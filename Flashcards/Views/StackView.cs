using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.Views
{
    public class StackView
    {
        public void Menu(DatabaseContext context)
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("MANAGE STACK MENU")
                    .PageSize(7)
                    .AddChoices(new[]
                    {
                    "1. Return to Main Menu", "2. Add Stack", "3. View Stack", "4. Update Stack", "5. Delete Stack"
                    }));
                var service = new StackService(context);
                if (int.Parse(choice.Substring(0, 1)) == 1) break;

                service.SelectOperation(int.Parse(choice.Substring(0, 1)));
            }
        }
    }
}



