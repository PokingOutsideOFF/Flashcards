using Spectre.Console;

namespace Flashcards.Views
{
    public class MainMenu
    {
       public void Menu()
       {
            while (true)
            {
                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("MAIN MENU")
                    .PageSize(5)
                    .AddChoices(new[]
                    {
                    "1. Manage Stacks", "2. Manage Flashcards", "3. Study", "4. View Study Session", "5. Exit"
                    }));

                if (int.Parse(choice.Substring(0, 1)) == 5)
                {
                    AnsiConsole.Markup("[red]Exiting.....[/]");
                    Thread.Sleep(1000);
                    return;
                }
                SelectView(int.Parse(choice.Substring(0, 1))); 
            }
       }

        public void SelectView(int choice)
        {
            DatabaseContext context = new DatabaseContext();

            switch (choice)
            {
                case 1:
                    var stackView = new StackView();
                    stackView.Menu(context);
                    break;
                case 2:
                    var flashcardView = new FlashcardView();
                    flashcardView.Menu(context);
                    break;
                case 5:
                    break;
            }
        }

    }
}

//MAINMENU -> View -> Service -> Repo 